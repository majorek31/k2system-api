using MediatR;
using WebAPI.Dtos;

namespace WebAPI.Features.User.Query.GetUserById;

public record GetUserByIdQuery(int UserId) : IRequest<UserDto?>;