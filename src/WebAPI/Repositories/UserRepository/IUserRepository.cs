using WebAPI.Entities;

namespace WebAPI.Repositories.UserRepository;

public interface IUserRepository : IRepository<User>
{
    Task<User?> GetUserByEmail(string email);

}