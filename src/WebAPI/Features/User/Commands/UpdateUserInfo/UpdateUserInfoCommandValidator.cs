using FluentValidation;
using WebAPI.Dtos;
using WebAPI.Repositories.UserRepository;

namespace WebAPI.Features.User.Commands.UpdateUserInfo;

public class UpdateUserInfoCommandValidator : AbstractValidator<UpdateUserInfoDto>
{
    public UpdateUserInfoCommandValidator(IUserRepository userRepository)
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
        

        RuleFor(x => x.FirstName)
            .NotEmpty().WithMessage("FirstName must not be empty");
        
        RuleFor(x => x.LastName)
            .NotEmpty().WithMessage("LastName must not be empty");
    }
}