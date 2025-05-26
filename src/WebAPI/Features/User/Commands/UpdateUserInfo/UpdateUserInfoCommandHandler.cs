using Mapster;
using MediatR;
using WebAPI.Common.Exceptions;
using WebAPI.Repositories.ScopeRepository;
using WebAPI.Repositories.UserRepository;
using WebAPI.Services;

namespace WebAPI.Features.User.Commands.UpdateUserInfo;

public class UpdateUserInfoCommandHandler(IUserRepository userRepository, IAuthService authService, IScopeRepository scopeRepository) : IRequestHandler<UpdateUserInfoCommand, Unit>
{
    public async Task<Unit> Handle(UpdateUserInfoCommand request, CancellationToken cancellationToken)
    {
        var loggedInUser = await authService.GetUser();
        if (loggedInUser == null)
            throw new NotFoundException();

        var writeUserScope = await scopeRepository.FindByValue("write:user") ?? throw new NotFoundException();
        var adminScope = await scopeRepository.FindByValue("admin") ?? throw new NotFoundException();

        var userHasScope = loggedInUser.Scopes.Any(s => s.Id == writeUserScope.Id || s.Id == adminScope.Id);
        if ((request.UserId != loggedInUser.Id) && !userHasScope)
            throw new NotFoundException();
        
        var targetUser = loggedInUser.Id != request.UserId ? await userRepository.GetByIdAsync(request.UserId) : loggedInUser;
        if (targetUser == null)
            throw new NotFoundException();

        targetUser.Email = request.UpdateUserInfoDto.Email;
        targetUser.FirstName = request.UpdateUserInfoDto.FirstName;
        targetUser.LastName = request.UpdateUserInfoDto.LastName;
        

        await userRepository.UpdateAsync(targetUser);
        return Unit.Value;
    }
}