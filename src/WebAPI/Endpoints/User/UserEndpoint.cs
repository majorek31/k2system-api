using MediatR;
using WebAPI.Dtos;
using WebAPI.Extensions;
using WebAPI.Features.User.Query.GetAllUsers;
using WebAPI.Features.User.Query.GetCurrentUser;

namespace WebAPI.Endpoints.User;

public class UserEndpoint : Endpoint
{
    public override void MapEndpoints(IEndpointRouteBuilder route)
    {
        var group = route.MapGroup("user");
        Configure<UserDto>(
            group.MapGet("/me", async (IMediator mediator) =>
                {
                    var result = await mediator.Send(new GetCurrentUserQuery());
                    return result;
                })
                .RequireAuthorization(),
            "Get current user",
            "User",
            "Get currently logged user profile"
        );
        Configure<IEnumerable<UserDto>>(
            group.MapGet("/", async (IMediator mediator) =>
                {
                    var result = await mediator.Send(new GetAllUsersQuery());
                    return result;
                })
                .RequireScope("read:user"),
            "GetAllUsers",
            "User",
            "Get all users"
        );
    }
}