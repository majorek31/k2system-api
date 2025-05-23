using Mapster;
using MediatR;
using WebAPI.Repositories.OrderRepository;
using WebAPI.Services;

namespace WebAPI.Features.Order.Commands.CreateOrder;

public class CreateOrderCommandHandler(IAuthService authService, IOrderRepository orderRepository) : IRequestHandler<CreateOrderCommand, Entities.Order>
{
    public async Task<Entities.Order> Handle(CreateOrderCommand request, CancellationToken cancellationToken)
    {
        var user = await authService.GetUser();
        if (user == null)
            throw new Exception("User not found");
        var order = request.Dto.Adapt<Entities.Order>();
        order.User = user;

        await orderRepository.CreateAsync(order);
        return order;
    }
}