using MediatR;
using WebAPI.Dtos;
using WebAPI.Extensions;
using WebAPI.Features.User.Commands.DeleteUser;
using WebAPI.Features.User.Commands.UpdateUserInfo;
using WebAPI.Features.User.Commands.UpdateUserScopes;
using WebAPI.Features.User.Query.GetAllUsers;
using WebAPI.Features.User.Query.GetCurrentUser;
using WebAPI.Features.User.Query.GetUserById;
using WebAPI.Features.User.Query.GetUserScopes;
using WebAPI.Services;

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
        Configure<UserDto>(
            group.MapGet("/{userId:int}", async (IMediator mediator, int userId) =>
            {
                var result = await mediator.Send(new GetUserByIdQuery(userId));
                return result is null ? Results.NotFound() : Results.Ok(result);
            }).RequireScope("read:user"),
            "Get user",
            "User",
            "Get user details"
        );
        Configure<Unit>(
            group.MapDelete("/{userId:int}", async (IMediator mediator, int userId) =>
            {
                await mediator.Send(new DeleteUserCommand(userId));
                return Results.Ok();
            }).RequireScope("write:user"),
            "Delete user",
            "User",
            "Deletes user from database"
        );

        Configure<UpdateUserInfoDto, Unit>(
            group.MapPatch("/{userId:int}", async (IMediator mediator, int userId, UpdateUserInfoDto dto) =>
            {
                await mediator.Send(new UpdateUserInfoCommand(dto, userId));
                return Results.Ok();
            }).RequireAuthorization(),
            "Update user",
            "User",
            "Updates user info. Can be accessed either with write:user scope or by the user itself"
        );

        Configure<UpdateUserScopesDto, Unit>(
            group.MapPatch("/{userId:int}/scope", async (IMediator mediator, int userId, UpdateUserScopesDto dto) =>
            {
                await mediator.Send(new UpdateUserScopesCommand(dto, userId));
                return Results.Ok();
            }).RequireScope("admin"),
            "Update scopes",
            "User",
            "Updates user scopes of desired user"
        );

        Configure<IEnumerable<ScopeDto>>(
            group.MapGet("/{userId:int}/scope", async (IMediator mediator, int userId) =>
            {
                var result = await mediator.Send(new GetUserScopesQuery(userId));
                return Results.Ok(result);
            }),
            "Get scopes",
            "User",
            "Get scopes of desired user"
        );
    }
}