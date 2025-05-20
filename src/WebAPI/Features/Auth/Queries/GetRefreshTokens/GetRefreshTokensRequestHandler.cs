using Mapster;
using MediatR;
using WebAPI.Dtos;
using WebAPI.Repositories.RefreshTokenRepository;
using WebAPI.Services;

namespace WebAPI.Features.Auth.Queries.GetRefreshTokens;

public class GetRefreshTokensRequestHandler(IRefreshTokenRepository refreshTokenRepository, IAuthService authService) : IRequestHandler<GetRefreshTokensRequest, IEnumerable<TokenDto>>
{
    public async Task<IEnumerable<TokenDto>> Handle(GetRefreshTokensRequest request, CancellationToken cancellationToken)
    {
        var user = await authService.GetUser();
        if (user is null)
            throw new Exception("User not authenticated");
        var tokens = await refreshTokenRepository.GetRefreshTokensByUser(user);
        return tokens.Adapt<IEnumerable<TokenDto>>();
    }
}