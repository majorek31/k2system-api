using MediatR;

namespace WebAPI.Features.Auth.Queries.GetRefreshTokens;

public record GetRefreshTokensRequest : IRequest<RefreshTokenDto>;