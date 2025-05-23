using Mapster;
using MediatR;
using WebAPI.Dtos;
using WebAPI.Repositories.OrderRepository;
using WebAPI.Repositories.ScopeRepository;
using WebAPI.Services;

namespace WebAPI.Features.Order.Queries.GetOrder;

public class GetOrderQueryHandler(IOrderRepository orderRepository,
    IAuthService authService,
    IScopeRepository scopeRepository) : IRequestHandler<GetOrderQuery, OrderDto?>
{
    public async Task<OrderDto?> Handle(GetOrderQuery request, CancellationToken cancellationToken)
    {
        var user = await authService.GetUser();
        if (user == null)
            throw new Exception("User not found");
        var order = await orderRepository.GetByIdAsync(request.OrderId);
        var userScopes = user.Scopes;
        var readOrderScope = await scopeRepository.FindByValue("read:scope") ?? throw new Exception("Scope not found");
        var adminScope = await scopeRepository.FindByValue("admin") ?? throw new Exception("Scope not found");

        var userHasScope = userScopes.Contains(readOrderScope) || userScopes.Contains(adminScope);
        if ((order != null && order.UserId != user.Id) && !userHasScope)
            return null;
        
        return order.Adapt<OrderDto>();
    }
}