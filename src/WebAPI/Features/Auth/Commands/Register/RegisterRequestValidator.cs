using FluentValidation;
using WebAPI.Repositories.UserRepository;

namespace WebAPI.Features.Auth.Commands.Register;

public class RegisterRequestValidator : AbstractValidator<RegisterRequest>
{
    public RegisterRequestValidator(IUserRepository userRepository)
    {
        RuleFor(x => x.Email)
            .EmailAddress().WithMessage("Invalid email format")
            .NotEmpty().WithMessage("Email must be provided")
            .MustAsync(async (email, cancellationToken) =>
            {
                var user = await userRepository.GetUserByEmail(email);
                return user is null;
            })
            .WithMessage("Email is in use");

        RuleFor(x => x.Password)
            .MinimumLength(8).WithMessage("Password must be at least 8 characters long")
            .NotEmpty().WithMessage("Password must not be empty");

        RuleFor(x => x.FirstName)
            .NotEmpty().WithMessage("FirstName must not be empty");
        
        RuleFor(x => x.LastName)
            .NotEmpty().WithMessage("LastName must not be empty");
    }
}