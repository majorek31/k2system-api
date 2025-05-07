namespace WebAPI.Endpoints;

public abstract class Endpoint : IEndpoint
{
    public abstract void MapEndpoints(IEndpointRouteBuilder route);

    protected RouteHandlerBuilder Configure<TResponse>(
        RouteHandlerBuilder builder,
        string name,
        string tag,
        string description = "") => builder
        .WithName(name)
        .WithTags(tag)
        .WithDescription(description)
        .Produces<TResponse>(StatusCodes.Status200OK)
        .WithOpenApi();
    
    protected RouteHandlerBuilder Configure<TRequest, TResponse>(
        RouteHandlerBuilder builder,
        string name,
        string tag,
        string description = "") where TRequest : notnull => builder
        .WithName(name)
        .WithTags(tag)
        .WithDescription(description)
        .Accepts<TRequest>("application/json")
        .Produces<TResponse>(StatusCodes.Status200OK)
        .WithOpenApi();
}