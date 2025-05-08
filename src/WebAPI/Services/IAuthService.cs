using WebAPI.Entities;

namespace WebAPI.Services;

public interface IAuthService
{
     public Task<string> CreateJsonWebToken(User user);
     public Task<bool> ValidateCredentials(string email, string password);
     public Task<RefreshToken> GenerateRefreshToken(User user, string? userAgent);
}