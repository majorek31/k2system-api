using MediatR;
using WebAPI.Dtos;

namespace WebAPI.Features.Auth.Queries.Login;

public record LoginRequest(LoginDto Dto, string? UserAgent) : IRequest<LoginResponse>;