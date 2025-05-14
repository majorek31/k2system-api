using Microsoft.EntityFrameworkCore;
using WebAPI.Database;
using WebAPI.Entities;

namespace WebAPI.Repositories.ScopeRepository;

public class ScopeRepository(AppDbContext context) : Repository<Scope>(context), IScopeRepository
{
    public async Task<IEnumerable<Scope>> GetScopesByUserAsync(User user)
    {
        var userWithScopes = await _context
            .Users
            .Include(u => u.Scopes)
            .FirstOrDefaultAsync(u => u.Id == user.Id);
        return userWithScopes?.Scopes ?? new();
    }
}