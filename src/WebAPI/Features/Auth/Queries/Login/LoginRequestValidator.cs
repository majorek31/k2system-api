using FluentValidation;
using WebAPI.Dtos;
using WebAPI.Services;

namespace WebAPI.Features.Auth.Queries.Login;

public class LoginRequestValidator : AbstractValidator<LoginDto>
{
    public LoginRequestValidator(IAuthService authService)
    {
        RuleFor(x => x.Email)
            .EmailAddress().WithMessage("Invalid email format")
            .NotEmpty().WithMessage("Email must be provided");

        RuleFor(x => x.Password)
            .MinimumLength(8).WithMessage("Password must be at least 8 characters long")
            .NotEmpty().WithMessage("Password must not be empty");

        RuleFor(x => x)
            .MustAsync(async (request, _) =>
                await authService.ValidateCredentials(request.Email, request.Password)).WithMessage("Invalid email or password");
    }
}