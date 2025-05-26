using MediatR;

namespace WebAPI.Features.User.Commands.DeleteUser;

public record DeleteUserCommand(int UserId) : IRequest<Unit>;