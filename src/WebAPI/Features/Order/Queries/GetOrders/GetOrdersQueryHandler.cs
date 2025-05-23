using Mapster;
using MediatR;
using WebAPI.Dtos;
using WebAPI.Repositories.OrderRepository;
using WebAPI.Services;

namespace WebAPI.Features.Order.Queries.GetOrders;

public class GetOrdersQueryHandler(IOrderRepository orderRepository, IAuthService authService) : IRequestHandler<GetOrdersQuery, IEnumerable<OrderDto>>
{
    public async Task<IEnumerable<OrderDto>> Handle(GetOrdersQuery request, CancellationToken cancellationToken)
    {
        var user = await authService.GetUser();
        if (user == null)
            throw new Exception("User not found");
        var orders = await orderRepository.GetOrdersByUser(user);
        return orders.Adapt<IEnumerable<OrderDto>>();
    }
}