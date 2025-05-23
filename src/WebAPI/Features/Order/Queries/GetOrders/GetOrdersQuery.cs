using MediatR;
using WebAPI.Dtos;

namespace WebAPI.Features.Order.Queries.GetOrders;

public record GetOrdersQuery() : IRequest<IEnumerable<OrderDto>>;
