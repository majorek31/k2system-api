using Mapster;
using MediatR;
using WebAPI.Entities;
using WebAPI.Repositories.UserRepository;

namespace WebAPI.Features.Auth.Commands.Register;

public class RegisterCommandHandler(IUserRepository userRepository) : IRequestHandler<RegisterCommand, Unit>
{
    public async Task<Unit> Handle(RegisterCommand request, CancellationToken cancellationToken)
    {
        var isFree = await userRepository.GetUserByEmail(request.Dto.Email) is null;
        if (!isFree)
        {
            throw new NotImplementedException();
        }

        var passwordHash = BCrypt.Net.BCrypt.HashPassword(request.Dto.Password);
        var user = request.Dto.Adapt<User>();
        user.PasswordHash = passwordHash;
        await userRepository.CreateAsync(user);
        return Unit.Value;
    }
}