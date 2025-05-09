using FluentValidation;
using WebAPI.Repositories.RefreshTokenRepository;

namespace WebAPI.Features.Auth.Queries.Refresh;

public class RefreshRequestValidator : AbstractValidator<RefreshDto>
{
    public RefreshRequestValidator(IRefreshTokenRepository refreshTokenRepository)
    {
        RuleFor(x => x.Token)
            .NotEmpty().WithMessage("Token must be provided")
            .MustAsync(async (token, _) =>
            {
                var refreshToken = await refreshTokenRepository.GetRefreshTokenByTokenAsync(token);

                return refreshToken is not null &&
                       !refreshToken.IsRevoked &&
                       refreshToken.ExpiresAt > DateTime.UtcNow;
            })
            .WithMessage("Token is invalid, revoked, or expired");

    }
}