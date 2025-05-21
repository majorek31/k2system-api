using Mapster;
using MediatR;
using WebAPI.Entities;
using WebAPI.Repositories.ScopeRepository;
using WebAPI.Repositories.UserRepository;

namespace WebAPI.Features.Auth.Commands.Register;

public class RegisterCommandHandler(IUserRepository userRepository, IScopeRepository scopeRepository) : IRequestHandler<RegisterCommand, Unit>
{
    public async Task<Unit> Handle(RegisterCommand request, CancellationToken cancellationToken)
    {
        var isPersonal = request.Dto.UserType.Equals("Personal", StringComparison.InvariantCultureIgnoreCase);
        Entities.User user = isPersonal ? request.Dto.Adapt<UserPersonal>() : request.Dto.Adapt<UserCompany>();
        user.UserType = isPersonal ? "Personal" : "Company";
        var passwordHash = BCrypt.Net.BCrypt.HashPassword(request.Dto.Password);
        user.Scopes.AddRange(new List<Scope>
        {
            await scopeRepository.FindByValue("write:review") ?? new Scope{Value = "write:review"},
        });
        user.PasswordHash = passwordHash;
        await userRepository.CreateAsync(user);
        return Unit.Value;
    }
}