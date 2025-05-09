namespace WebAPI.Features.Auth.Queries.GetRefreshTokens;

public record TokenDto(int Id, string UserAgent, DateTime ExpiresAt, DateTime CreatedAt);
public record RefreshTokenDto(IEnumerable<TokenDto> Tokens);