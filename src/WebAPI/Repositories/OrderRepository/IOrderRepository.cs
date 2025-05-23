using WebAPI.Dtos;
using WebAPI.Entities;
using WebAPI.Features.Order.Queries.GetOrders;

namespace WebAPI.Repositories.OrderRepository;

public interface IOrderRepository : IRepository<Order>
{
    Task<IEnumerable<Order>> GetOrdersByUser(User user);
}