using Mapster;
using MediatR;
using WebAPI.Dtos;
using WebAPI.Repositories.OrderRepository;

namespace WebAPI.Features.Order.Queries.GetAllOrders;

public class GetAllOrdersQueryHandler(IOrderRepository orderRepository) : IRequestHandler<GetAllOrdersQuery, List<OrderDto>>
{
    public async Task<List<OrderDto>> Handle(GetAllOrdersQuery request, CancellationToken cancellationToken)
    {
        var orders = await orderRepository.GetAsync();
        return orders.Adapt<List<OrderDto>>();
    }
}