using Mapster;
using MediatR;
using WebAPI.Dtos;
using WebAPI.Entities;
using WebAPI.Services;

namespace WebAPI.Features.User.Query.GetCurrentUser;

public class GetCurrentUserQueryHandler(IAuthService authService) : IRequestHandler<GetCurrentUserQuery, UserDto>
{
    public async Task<UserDto> Handle(GetCurrentUserQuery request, CancellationToken cancellationToken)
    {
        var user = await authService.GetUser();
        if (user is null)
            throw new Exception("User not found");
        return user switch
        {
            UserCompany company => company.Adapt<UserDto>(),
            UserPersonal personal => personal.Adapt<UserDto>(),
            _ => throw new Exception("User not found")
        };
    }
}