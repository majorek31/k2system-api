using WebAPI.Services;

namespace WebAPI.Extensions;

public static class EndpointAuthorizationExtensions
{
    public static RouteHandlerBuilder RequireScope(this RouteHandlerBuilder builder, string scope)
    {
        builder.RequireAuthorization();
        builder.Produces(StatusCodes.Status401Unauthorized);
        builder.Produces(StatusCodes.Status403Forbidden);
        builder.AddEndpointFilterFactory((_, next) =>
        {
            return async context =>
            {
                var scopeClaim = context.HttpContext.User
                    .FindAll("scope")
                    .Select(c => c.Value)
                    .ToList();
                
                if (scopeClaim.Contains(scope))
                    return await next(context);
                
                return Results.Forbid();
            };
        });
        return builder;
    }
}