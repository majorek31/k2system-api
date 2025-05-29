using Mapster;
using MediatR;
using WebAPI.Common.Exceptions;
using WebAPI.Dtos;
using WebAPI.Repositories.OrderRepository;
using WebAPI.Repositories.UserRepository;

namespace WebAPI.Features.Order.Queries.GetOrders;

public class GetOrdersByUserQueryHandler(IOrderRepository orderRepository, IUserRepository userRepository) : IRequestHandler<GetOrdersByUserQuery, IEnumerable<OrderDto>>
{
    public async Task<IEnumerable<OrderDto>> Handle(GetOrdersByUserQuery request, CancellationToken cancellationToken)
    {
        var user = await userRepository.GetByIdAsync(request.UserId);
        if (user is null)
            throw new NotFoundException();
        var orders = await orderRepository.GetOrdersByUser(user);
        return orders.Adapt<IEnumerable<OrderDto>>();
    }
}