using WebAPI.Database;
using WebAPI.Entities;

namespace WebAPI.Repositories.OrderRepository;

public class OrderRepository(AppDbContext context) : Repository<Order>(context), IOrderRepository
{
    
}