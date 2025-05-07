using WebAPI.Entities;

namespace WebAPI.Services;

public interface IAuthService
{
     public Task<string> CreateJsonWebToken(User user);
}