using MediatR;
using WebAPI.Dtos;

namespace WebAPI.Features.User.Query.GetCurrentUser;

public record GetCurrentUserQuery() : IRequest<UserDto>;