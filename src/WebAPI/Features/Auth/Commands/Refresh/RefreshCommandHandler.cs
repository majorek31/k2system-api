using MediatR;
using WebAPI.Features.Auth.Queries.Login;
using WebAPI.Repositories.RefreshTokenRepository;
using WebAPI.Services;

namespace WebAPI.Features.Auth.Commands.Refresh;

public class RefreshCommandHandler(IRefreshTokenRepository refreshTokenRepository, IAuthService authService) : IRequestHandler<RefreshCommand, LoginResponse>
{
    public async Task<LoginResponse> Handle(RefreshCommand command, CancellationToken cancellationToken)
    {
        var token = await refreshTokenRepository.GetRefreshTokenByTokenAsync(command.Dto.Token);
        if (token is null)
            throw new Exception("Token validation error");
        var jsonWebToken = await authService.CreateJsonWebToken(token.User);
        return new LoginResponse(jsonWebToken, token.Token);
    }
}