namespace WebAPI.Endpoints;

public interface IEndpoint
{
    void MapEndpoints(IEndpointRouteBuilder route);
}