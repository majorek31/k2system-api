using MediatR;
using WebAPI.Features.Auth.Queries.Login;

namespace WebAPI.Features.Auth.Queries.Refresh;

public record RefreshRequest(RefreshDto Dto) : IRequest<LoginResponse>; 
