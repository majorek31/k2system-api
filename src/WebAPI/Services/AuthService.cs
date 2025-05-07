using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using WebAPI.Entities;

namespace WebAPI.Services;

public class AuthService(IConfiguration configuration) : IAuthService 
{
    private static readonly JwtSecurityTokenHandler JwtSecurityTokenHandler = new();
    public Task<string> CreateJsonWebToken(User user)
    {
        var key = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(configuration["JWT:Key"] ?? throw new Exception("JWT Key not provided in appsettings.json")));
        var credentials = new SigningCredentials(key, SecurityAlgorithms.Sha256);
        var claims = new [] {
            new Claim(ClaimTypes.Email, user.Email)
        };
        var payload = new JwtPayload(claims);
        var header = new JwtHeader(credentials);
        var token = new JwtSecurityToken(header, payload);

        return Task.FromResult(JwtSecurityTokenHandler.WriteToken(token));
    }
}