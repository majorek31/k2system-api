using MediatR;
using WebAPI.Dtos;
using WebAPI.Features.Auth.Queries.Login;

namespace WebAPI.Features.Auth.Commands.Refresh;

public record RefreshCommand(RefreshDto Dto) : IRequest<LoginResponse>; 
