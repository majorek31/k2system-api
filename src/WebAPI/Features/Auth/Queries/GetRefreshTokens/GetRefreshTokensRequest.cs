using MediatR;
using WebAPI.Dtos;

namespace WebAPI.Features.Auth.Queries.GetRefreshTokens;

public record GetRefreshTokensRequest : IRequest<IEnumerable<TokenDto>>;