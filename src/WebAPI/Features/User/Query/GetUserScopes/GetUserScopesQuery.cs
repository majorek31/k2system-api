using MediatR;
using WebAPI.Dtos;

namespace WebAPI.Features.User.Query.GetUserScopes;

public record GetUserScopesQuery(int UserId) : IRequest<IEnumerable<ScopeDto>>;