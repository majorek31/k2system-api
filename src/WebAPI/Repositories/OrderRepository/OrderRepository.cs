using Microsoft.EntityFrameworkCore;
using WebAPI.Database;
using WebAPI.Dtos;
using WebAPI.Entities;

namespace WebAPI.Repositories.OrderRepository;

public class OrderRepository(AppDbContext context) : Repository<Order>(context), IOrderRepository
{
    public new async Task<IEnumerable<Order>> GetAsync()
    {
        return await context
            .Orders
            .Include(o => o.OrderItems)
            .ThenInclude(oi => oi.Product)
            .AsNoTracking()
            .ToListAsync();
    }

    public new async Task<Order?> GetByIdAsync(int id)
    {
        return await context
            .Orders
            .Include(o => o.OrderItems)
            .ThenInclude(oi => oi.Product)
            .FirstOrDefaultAsync(x => x.Id == id);
    }

    public async Task<IEnumerable<Order>> GetOrdersByUser(User user)
    {
        return await context.Orders
            .Include(o => o.OrderItems)
            .ThenInclude(oi => oi.Product)
            .Where(o => o.UserId == user.Id)
            .ToListAsync();
    }
}