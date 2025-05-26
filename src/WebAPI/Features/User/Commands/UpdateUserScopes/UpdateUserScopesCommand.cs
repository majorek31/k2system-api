using MediatR;
using WebAPI.Dtos;

namespace WebAPI.Features.User.Commands.UpdateUserScopes;

public record UpdateUserScopesCommand(UpdateUserScopesDto UpdateUserScopesDto, int UserId) : IRequest<Unit>;