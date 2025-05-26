using MediatR;
using WebAPI.Dtos;

namespace WebAPI.Features.User.Commands.UpdateUserInfo;

public record UpdateUserInfoCommand(UpdateUserInfoDto UpdateUserInfoDto, int UserId) : IRequest<Unit>;