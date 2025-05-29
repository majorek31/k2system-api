using Mapster;
using MediatR;
using WebAPI.Common.Exceptions;
using WebAPI.Dtos;
using WebAPI.Entities;
using WebAPI.Repositories.ScopeRepository;
using WebAPI.Repositories.UserRepository;

namespace WebAPI.Features.User.Query.GetUserScopes;

public class GetUserScopesQueryHandler(IScopeRepository scopeRepository, IUserRepository userRepository) : IRequestHandler<GetUserScopesQuery, IEnumerable<ScopeDto>>
{
    public async Task<IEnumerable<ScopeDto>> Handle(GetUserScopesQuery request, CancellationToken cancellationToken)
    { 
        var user = await userRepository.GetByIdAsync(request.UserId);
        if (user == null)
            throw new NotFoundException();
        var scopes = await scopeRepository.GetScopesByUserAsync(user);
        return scopes.Adapt<IEnumerable<ScopeDto>>();
    }
}