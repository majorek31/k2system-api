using FluentValidation;
using WebAPI.Dtos;
using WebAPI.Repositories.ScopeRepository;

namespace WebAPI.Features.User.Commands.UpdateUserScopes;

public class UpdateUserScopesCommandValidator : AbstractValidator<UpdateUserScopesDto>
{
    public UpdateUserScopesCommandValidator(IScopeRepository scopeRepository)
    {
        RuleFor(x => x.Scopes)
            .MustAsync(async (scopes, _) =>
            {
                foreach (var scope in scopes)
                {
                    if (await scopeRepository.GetByIdAsync(scope.Id) is null)
                        return false;
                }
                return true;
            }).WithMessage("Invalid scopes");
    }
}