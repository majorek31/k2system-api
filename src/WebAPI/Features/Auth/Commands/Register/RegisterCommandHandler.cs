using Mapster;
using MediatR;
using WebAPI.Entities;
using WebAPI.Repositories.UserRepository;

namespace WebAPI.Features.Auth.Commands.Register;

public class RegisterCommandHandler(IUserRepository userRepository) : IRequestHandler<RegisterCommand, Unit>
{
    public async Task<Unit> Handle(RegisterCommand request, CancellationToken cancellationToken)
    {
        var isPersonal = request.Dto.UserType.Equals("Personal", StringComparison.InvariantCultureIgnoreCase);
        Entities.User user = isPersonal ? request.Dto.Adapt<UserPersonal>() : request.Dto.Adapt<UserCompany>();
        user.UserType = isPersonal ? "Personal" : "Company";
        var passwordHash = BCrypt.Net.BCrypt.HashPassword(request.Dto.Password);
        
        user.PasswordHash = passwordHash;
        await userRepository.CreateAsync(user);
        return Unit.Value;
    }
}