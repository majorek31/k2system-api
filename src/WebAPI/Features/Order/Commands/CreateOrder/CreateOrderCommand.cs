using MediatR;
using WebAPI.Dtos;

namespace WebAPI.Features.Order.Commands.CreateOrder;

public record CreateOrderCommand(CreateOrderDto Dto) : IRequest<Entities.Order>;