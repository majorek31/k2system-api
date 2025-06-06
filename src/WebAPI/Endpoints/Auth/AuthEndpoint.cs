﻿using MediatR;
using WebAPI.Dtos;
using WebAPI.Features.Auth.Commands;
using WebAPI.Features.Auth.Commands.Register;
using WebAPI.Features.Auth.Queries.Login;
using WebAPI.Features.Auth.Commands.Refresh;
using WebAPI.Features.Auth.Queries.GetRefreshTokens;

namespace WebAPI.Endpoints.Auth;

public class AuthEndpoint : Endpoint
{
    public override void MapEndpoints(IEndpointRouteBuilder route)
    {
        var group = route.MapGroup("/auth");

        Configure<RegisterDto, Unit>(
            group.MapPost("/register", async (RegisterDto request, IMediator mediator) =>
            {
                await mediator.Send(new RegisterCommand(request));
                return Results.Created();
            })
            .Produces(StatusCodes.Status201Created),
            "Register endpoint",
            "Auth",
            "Allows users to register onto the website"
        );
        
        Configure<LoginDto, LoginResponse>(
            group.MapGet("/login", async ([AsParameters] LoginDto request, IMediator mediator, HttpContext context) =>
            {
                var userAgent = context.Request.Headers.UserAgent;
                if (userAgent.Count > 255)
                    return Results.StatusCode(StatusCodes.Status413PayloadTooLarge);
                var result = await mediator.Send(new LoginRequest(request, userAgent));
                return Results.Ok(result);
            })
            .Produces(StatusCodes.Status413PayloadTooLarge),
            "Login endpoint",
            "Auth", 
            "Allows users to request access token as well as refresh token"
        );

        Configure<RefreshDto, LoginResponse>(
            group.MapGet("/refresh", async ([AsParameters] RefreshDto request, IMediator mediator) =>
            {
                var result = await mediator.Send(new RefreshCommand(request));
                return Results.Ok(result);
            }),
            "Refresh",
            "Auth",
            "Allows users to refresh JWT token using provided RefreshToken"
        );

        Configure<IEnumerable<TokenDto>>(
            group.MapGet("/refreshTokens", async (IMediator mediator) =>
            {
                var result = await mediator.Send(new GetRefreshTokensRequest());
                return Results.Ok(result);
            })
            .RequireAuthorization(),
            "Get refresh tokens",
            "Auth",
            "Allows to retrieve all valid refresh tokens (without the token itself) that are were requested by user");
    }
}