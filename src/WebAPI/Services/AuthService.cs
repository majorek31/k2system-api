using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Writers;
using WebAPI.Entities;
using WebAPI.Repositories.RefreshTokenRepository;
using WebAPI.Repositories.ScopeRepository;
using WebAPI.Repositories.UserRepository;
using Scope = WebAPI.Entities.Scope;

namespace WebAPI.Services;

public class AuthService(IConfiguration configuration,
    IUserRepository userRepository,
    IRefreshTokenRepository refreshTokenRepository,
    IHttpContextAccessor contextAccessor,
    IScopeRepository scopeRepository) : IAuthService 
{
    private static readonly JwtSecurityTokenHandler JwtSecurityTokenHandler = new();
    public async Task<string> CreateJsonWebToken(User user)
    {
        var key = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(configuration["JWT:Key"] ?? throw new Exception("JWT Key not provided in appsettings.json")));
        var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
        var userScopes = await scopeRepository.GetScopesByUserAsync(user);
        var scopeClaims = userScopes
            .Select(s => new Claim("scope", s.Value));
        var claims = new [] {
            // new Claim(JwtRegisteredClaimNames.Sub, user.Id.ToString()), // dont feel like using ID as subject would be a big improvement here
            new Claim(JwtRegisteredClaimNames.Email, user.Email),
            // new Claim("scope", "read:content")
        }.Concat(scopeClaims);
        
        var token = new JwtSecurityToken(
            claims: claims,
            issuer: configuration["JWT:Issuer"] ?? "k2system-api",
            audience: configuration["JWT:Audience"] ?? throw new Exception("JWT:Audience not provided"),
            notBefore: DateTime.UtcNow,
            expires: DateTime.UtcNow.AddHours(1),
            signingCredentials: credentials
        );

        return JwtSecurityTokenHandler.WriteToken(token);
    }

    public async Task<bool> ValidateCredentials(string email, string password)
    {
        var user = await userRepository.GetUserByEmail(email);
        return user is not null && BCrypt.Net.BCrypt.Verify(password, user.PasswordHash);
    }

    public async Task<RefreshToken> GenerateRefreshToken(User user, string? userAgent)
    {
        var token = Convert.ToBase64String(RandomNumberGenerator.GetBytes(32));
        var refreshToken = new RefreshToken()
        {
            Token = token,
            UserId = user.Id,
            ExpiresAt = DateTime.UtcNow.AddDays(30),
            UserAgent = userAgent ?? "Unavailable",
        };
        await refreshTokenRepository.CreateAsync(refreshToken);
        return refreshToken;
    }

    public async Task<User?> GetUser()
    {
        var email = contextAccessor
            .HttpContext?
            .User ?
            .Claims?
            .FirstOrDefault(c => c.Type == ClaimTypes.Email)?
            .Value;
        if (email is null)
            return null;

        var user = await userRepository.GetUserByEmail(email);
        return user;
    }
}