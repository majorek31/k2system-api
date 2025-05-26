using MediatR;
using WebAPI.Dtos;
using WebAPI.Extensions;
using WebAPI.Features.Order.Commands.CreateOrder;
using WebAPI.Features.Order.Queries.GetOrder;
using WebAPI.Features.Order.Queries.GetOrders;

namespace WebAPI.Endpoints.Order;

public class OrderEndpoint : Endpoint
{
    public override void MapEndpoints(IEndpointRouteBuilder route)
    {
        var group = route.MapGroup("/order");
        Configure<CreateOrderDto, Unit>(
            group.MapPost("/", async (CreateOrderDto dto, IMediator mediator) =>
            {
                var result = await mediator.Send(new CreateOrderCommand(dto));
                var newLocation = "/order/" + result.Id;
                return Results.Created(newLocation, new());
            }).RequireAuthorization(),
            "CreateOrder",
            "Order",
            "Creates order with specified products"
        );
        Configure<IEnumerable<OrderDto>>(
            group.MapGet("/", async (IMediator mediator) =>
            {
                var result = await mediator.Send(new GetOrdersQuery());
                return Results.Ok(result);
            }),
            "GetOrders",
            "Order",
            "Returns all orders of the authenticated user");
        Configure<OrderDto>(
            group.MapGet("/{orderId:int}", async (int orderId, IMediator mediator) =>
            {
                var result = await mediator.Send(new GetOrderQuery(orderId));
                return result is null ? Results.NotFound() : Results.Ok(result);
            })
            .RequireAuthorization(),
            "GetOrder", 
            "Order",
            "Return order based on it's id");
    }
}