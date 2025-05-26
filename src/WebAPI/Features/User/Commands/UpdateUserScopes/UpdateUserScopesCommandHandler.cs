using Mapster;
using MediatR;
using WebAPI.Common.Exceptions;
using WebAPI.Entities;
using WebAPI.Repositories.UserRepository;

namespace WebAPI.Features.User.Commands.UpdateUserScopes;

public class UpdateUserScopesCommandHandler(IUserRepository userRepository) : IRequestHandler<UpdateUserScopesCommand, Unit>
{
    public async Task<Unit> Handle(UpdateUserScopesCommand request, CancellationToken cancellationToken)
    {
        var user = await userRepository.GetByIdAsync(request.UserId);
        if (user is null)
            throw new NotFoundException();
        user.Scopes.Clear();
        foreach (var scope in request.UpdateUserScopesDto.Scopes.Adapt<List<Scope>>())
        {
            user.Scopes.Add(scope);
        }
        await userRepository.UpdateAsync(user);
        return Unit.Value;
    }
}