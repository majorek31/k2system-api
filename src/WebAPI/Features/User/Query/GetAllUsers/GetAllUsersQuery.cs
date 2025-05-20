using MediatR;
using WebAPI.Dtos;

namespace WebAPI.Features.User.Query.GetAllUsers;

public record GetAllUsersQuery() : IRequest<IEnumerable<UserDto>>;