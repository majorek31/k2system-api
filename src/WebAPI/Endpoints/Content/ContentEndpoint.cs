using MediatR;
using WebAPI.Features.Content.Queries.GetTranslationsForPage;

namespace WebAPI.Endpoints.Content;

public class ContentEndpoint : Endpoint
{
    public override void MapEndpoints(IEndpointRouteBuilder route)
    {
        var group = route.MapGroup("/content");

        Configure<TranslationsResponseDto>(
            group.MapGet("/{page}", async (string page, string? lang, HttpContext context, IMediator mediator) =>
            {
                var result = await mediator.Send(new GetTranslationsForPageRequest(page, lang ?? "en"));
                return Results.Ok(result);
            }),
            "Get translations for page",
            "Content",
            "Responds with content for desired page in desired language");
    }
}