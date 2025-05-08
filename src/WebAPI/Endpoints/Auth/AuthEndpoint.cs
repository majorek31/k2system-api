using MediatR;
using WebAPI.Features.Auth.Commands;
using WebAPI.Features.Auth.Commands.Register;

namespace WebAPI.Endpoints.Auth;

public class AuthEndpoint : Endpoint
{
    public override void MapEndpoints(IEndpointRouteBuilder route)
    {
        var group = route.MapGroup("/auth");

        Configure<RegisterDto, Unit>(
            group.MapPost("/register", async (RegisterDto request, IMediator mediator) =>
            {
                var result = await mediator.Send(new RegisterCommand(request));
                return Results.Ok(result);
            }),
            "Register endpoint",
            "Auth",
            "Allows users to register onto the website"
        );
    }
}