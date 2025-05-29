using MediatR;
using WebAPI.Common.Exceptions;
using WebAPI.Entities;
using WebAPI.Repositories.OrderRepository;
using WebAPI.Repositories.ScopeRepository;
using WebAPI.Services;

namespace WebAPI.Features.Order.Commands.UpdateOrderStatus;

public class UpdateOrderStatusCommandHandler(IOrderRepository orderRepository,
    IAuthService authService,
    IScopeRepository scopeRepository) : IRequestHandler<UpdateOrderStatusCommand, Entities.Order>
{
    public async Task<Entities.Order> Handle(UpdateOrderStatusCommand request, CancellationToken cancellationToken)
    {
        var order = await orderRepository.GetByIdAsync(request.OrderId);
        var user = await authService.GetUser();
        if (order is null)
            throw new NotFoundException();
        if (user is null)
            throw new Exception("User not authenticated");
        var writeOrderScope = await scopeRepository.FindByValue("write:order") ?? throw new Exception("write:order scope not found");
        var adminScope = await scopeRepository.FindByValue("admin") ?? throw new Exception("admin scope not found");
        if (order.UserId != user.Id && (!user.Scopes.Contains(writeOrderScope) || !user.Scopes.Contains(adminScope)))
        {
            throw new NotFoundException();
        }
        order.OrderStatus = Enum.Parse<OrderStatus>(request.Dto.OrderStatus);
        await orderRepository.UpdateAsync(order);
        return order;
    }
}