using MediatR;
using WebAPI.Common.Exceptions;
using WebAPI.Repositories.UserRepository;

namespace WebAPI.Features.User.Commands.DeleteUser;

public class DeleteUserCommandHandler(IUserRepository userRepository) : IRequestHandler<DeleteUserCommand, Unit>
{
    public async Task<Unit> Handle(DeleteUserCommand request, CancellationToken cancellationToken)
    {
        var user = await userRepository.GetByIdAsync(request.UserId);
        if (user is null)
            throw new NotFoundException();
        await userRepository.DeleteAsync(user);
        return Unit.Value;
    }
}