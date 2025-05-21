using Microsoft.EntityFrameworkCore;
using WebAPI.Database;
using WebAPI.Entities;

namespace WebAPI.Repositories.ScopeRepository;

public class ScopeRepository(AppDbContext context) : Repository<Scope>(context), IScopeRepository
{
    public async Task<Scope?> FindByValue(string name)
    {
        return await context.Scopes.FirstOrDefaultAsync(s => s.Value == name);
    }

    public async Task<IEnumerable<Scope>> GetScopesByUserAsync(User user)
    {
        var userWithScopes = await context
            .Users
            .Include(u => u.Scopes)
            .FirstOrDefaultAsync(u => u.Id == user.Id);
        return userWithScopes?.Scopes ?? new();
    }
}