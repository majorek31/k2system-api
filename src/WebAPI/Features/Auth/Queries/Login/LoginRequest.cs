using MediatR;

namespace WebAPI.Features.Auth.Queries.Login;

public record LoginRequest(LoginDto Dto, string? UserAgent) : IRequest<LoginResponse>;