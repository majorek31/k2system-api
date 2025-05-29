using MediatR;
using WebAPI.Dtos;

namespace WebAPI.Features.Order.Queries.GetOrders;

public record GetOrdersByUserQuery(int UserId) : IRequest<IEnumerable<OrderDto>>;
