using MediatR;
using WebAPI.Features.Auth.Commands;
using WebAPI.Features.Auth.Commands.Register;

namespace WebAPI.Endpoints.Auth;

public class AuthEndpoint : Endpoint
{
    public override void MapEndpoints(IEndpointRouteBuilder route)
    {
        var group = route.MapGroup("/auth");

        Configure<RegisterRequest, Unit>(
            group.MapPost("/register", async (RegisterRequest request, IMediator mediator) =>
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