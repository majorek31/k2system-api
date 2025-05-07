using Microsoft.EntityFrameworkCore;
using WebAPI.Database;
using WebAPI.Entities;

namespace WebAPI.Repositories.UserRepository;

public class UserRepository(AppDbContext context) : Repository<User>(context), IUserRepository
{
    public async Task<User?> GetUserByEmail(string email)
    {
        return await _context.Users.FirstOrDefaultAsync(x => x.Email == email);
    }
}