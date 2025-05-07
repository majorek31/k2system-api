using MediatR;

namespace WebAPI.Features.Auth.Commands.Register;

public record RegisterCommand(RegisterRequest Dto) : IRequest<Unit>;