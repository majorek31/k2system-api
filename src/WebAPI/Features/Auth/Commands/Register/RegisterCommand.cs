using MediatR;

namespace WebAPI.Features.Auth.Commands.Register;

public record RegisterCommand(RegisterDto Dto) : IRequest<Unit>;