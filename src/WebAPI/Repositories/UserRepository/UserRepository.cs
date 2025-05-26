using Microsoft.EntityFrameworkCore;
using WebAPI.Database;
using WebAPI.Entities;

namespace WebAPI.Repositories.UserRepository;

public class UserRepository(AppDbContext context) : Repository<User>(context), IUserRepository
{
    public new async Task<User?> GetByIdAsync(int id)
    {
        return await context.Set<User>().
            Include(u => u.Scopes).
            FirstOrDefaultAsync(x => x.Id == id);
    }

    public async Task<User?> GetUserByEmail(string email)
    {
        return await context.Users.FirstOrDefaultAsync(x => x.Email == email);
    }
}