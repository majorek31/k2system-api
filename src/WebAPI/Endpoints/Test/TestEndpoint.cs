using WebAPI.Models.Test;

namespace WebAPI.Endpoints.Test;

public class TestEndpoint : Endpoint
{
    public override void MapEndpoints(IEndpointRouteBuilder route)
    {
        var group = route.MapGroup("/test");

        Configure<TestResponse>(
            group.MapGet("/hello", async () => new TestResponse("Hello m8", DateTime.UtcNow)),
            name: "HelloEndpoint",
            tag: "Test",
            description: "Returns the current UTC timestamp with a greeting."
        );
    }
}