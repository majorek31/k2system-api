using Microsoft.EntityFrameworkCore;
using WebAPI.Database;
using WebAPI.Entities;

namespace WebAPI.Repositories.RefreshTokenRepository;

public class RefreshTokenRepository(AppDbContext context) : Repository<RefreshToken>(context), IRefreshTokenRepository
{
    public async Task<RefreshToken?> GetRefreshTokenByTokenAsync(string token)
    {
        return await _context.RefreshTokens
            .Include(x => x.User)
            .FirstOrDefaultAsync(x => x.Token == token);
    }

    public async Task<IEnumerable<RefreshToken>> GetRefreshTokensByUser(User user)
    {
        var tokens = await context
            .RefreshTokens
            .Where(x => x.UserId == user.Id && x.IsRevoked == false)
            .AsNoTracking()
            .ToListAsync();
        return tokens;
    }
}