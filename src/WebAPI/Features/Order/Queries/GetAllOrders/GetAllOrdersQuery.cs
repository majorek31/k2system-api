using MediatR;
using WebAPI.Dtos;

namespace WebAPI.Features.Order.Queries.GetAllOrders;

public record GetAllOrdersQuery() : IRequest<List<OrderDto>>;