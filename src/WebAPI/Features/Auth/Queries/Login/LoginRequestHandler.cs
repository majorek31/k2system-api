using MediatR;
using WebAPI.Repositories.RefreshTokenRepository;
using WebAPI.Repositories.UserRepository;
using WebAPI.Services;

namespace WebAPI.Features.Auth.Queries.Login;

public class LoginRequestHandler(IUserRepository userRepository, IAuthService authService) : IRequestHandler<LoginRequest, LoginResponse>
{
    public async Task<LoginResponse> Handle(LoginRequest request, CancellationToken cancellationToken)
    {
        var user = await userRepository.GetUserByEmail(request.Dto.Email);
        if (user is null)
            throw new Exception("Issue while validating LoginRequest");
        var accessToken = await authService.CreateJsonWebToken(user);
        var refreshToken = await authService.GenerateRefreshToken(user, request.UserAgent);
        return new LoginResponse(accessToken, refreshToken.Token);
    }
}