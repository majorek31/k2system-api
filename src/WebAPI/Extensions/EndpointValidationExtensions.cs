using FluentValidation;

namespace WebAPI.Extensions;

public static class EndpointValidationExtensions
{
    public static RouteHandlerBuilder WithValidation<TRequest>(this RouteHandlerBuilder builder)
        where TRequest : notnull
    {
        builder.AddEndpointFilterFactory((_, next) =>
        {
            return async context =>
            {
                var request = (TRequest?)context.Arguments.FirstOrDefault(arg => arg?.GetType() == typeof(TRequest));
                if (request is null)
                    return Results.BadRequest();

                var validator = context.HttpContext.RequestServices.GetService<IValidator<TRequest>>();
                
                if (validator is null)
                    return await next(context);

                var result = await validator.ValidateAsync(request);

                if (result.IsValid) return await next(context);
                
                var errorList = result.Errors.Select(x => new { x.PropertyName, x.ErrorMessage });
                return Results.BadRequest(new { errors = errorList});

            };
        });
        return builder;
    }
}