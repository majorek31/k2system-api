using MediatR;
using WebAPI.Dtos;

namespace WebAPI.Features.Order.Queries.GetOrder;

public record GetOrderQuery(int OrderId) : IRequest<OrderDto?>;