using Mapster;
using MediatR;
using WebAPI.Repositories.RefreshTokenRepository;
using WebAPI.Services;

namespace WebAPI.Features.Auth.Queries.GetRefreshTokens;

public class GetRefreshTokensRequestHandler(IRefreshTokenRepository refreshTokenRepository, IAuthService authService) : IRequestHandler<GetRefreshTokensRequest, RefreshTokenDto>
{
    public async Task<RefreshTokenDto> Handle(GetRefreshTokensRequest request, CancellationToken cancellationToken)
    {
        var user = await authService.GetUser();
        if (user is null)
            throw new Exception("User not authenticated");
        var tokens = await refreshTokenRepository.GetRefreshTokensByUser(user);
        var tokensDtos = tokens
            .Select(x => x.Adapt<TokenDto>());
        return new RefreshTokenDto(tokensDtos);
    }
}