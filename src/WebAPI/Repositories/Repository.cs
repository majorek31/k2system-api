using Microsoft.EntityFrameworkCore;
using WebAPI.Common;
using WebAPI.Database;

namespace WebAPI.Repositories;

public class Repository<T>(AppDbContext context) : IRepository<T>
    where T : BaseEntity
{

    public async Task<IEnumerable<T>> GetAsync()
    {
        return await context.Set<T>().AsNoTracking().ToListAsync();
    }

    public async Task<T?> GetByIdAsync(int id)
    {
        return await context.Set<T>().AsNoTracking().FirstOrDefaultAsync(x => x.Id == id);
    }

    public async Task CreateAsync(T entity)
    {
        await context.AddAsync(entity);
        await context.SaveChangesAsync();
    }

    public async Task DeleteAsync(T entity)
    {
        context.Remove(entity);
        await context.SaveChangesAsync();
    }

    public async Task UpdateAsync(T entity)
    {
        context.Entry(entity).State = EntityState.Modified;
        await context.SaveChangesAsync();
    }

    public async Task<int> CountAsync()
    {
        return await context.Set<T>().CountAsync();
    }
}