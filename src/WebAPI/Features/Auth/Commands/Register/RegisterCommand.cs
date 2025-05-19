using MediatR;
using WebAPI.Dtos;

namespace WebAPI.Features.Auth.Commands.Register;

public record RegisterCommand(RegisterDto Dto) : IRequest<Unit>;