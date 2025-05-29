using MediatR;
using WebAPI.Dtos;

namespace WebAPI.Features.Order.Commands.UpdateOrderStatus;

public record UpdateOrderStatusCommand(UpdateOrderStatusDto Dto, int OrderId) : IRequest<Entities.Order>;