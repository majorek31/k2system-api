namespace WebAPI.Features.Auth.Queries.Login;

public record LoginResponse(string AccessToken, string RefreshToken);