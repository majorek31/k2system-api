using System.Reflection;
using WebAPI.Endpoints;

namespace WebAPI.Extensions;

public static class EndpointExtensions
{
    public static IServiceCollection AddEndpoints(this IServiceCollection services)
    {
        var endpointType = typeof(IEndpoint);
        var endpoints = Assembly
            .GetExecutingAssembly()
            .GetTypes()
            .Where(x => x.IsAssignableTo(endpointType) && x is { IsAbstract: false, IsInterface: false });

        foreach (var endpoint in endpoints)
        {
            services.AddSingleton(endpointType, endpoint);
        }
        
        return services;
    }

    public static WebApplication MapEndpoints(this WebApplication app, IEndpointRouteBuilder? route = null)
    {
        var services = app.Services.GetRequiredService<IEnumerable<IEndpoint>>();
        
        foreach (var service in services)
        {
            service.MapEndpoints(route ?? app);
        }
        return app;
    }
}