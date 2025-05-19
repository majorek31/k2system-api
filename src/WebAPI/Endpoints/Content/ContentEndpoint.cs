using MediatR;
using WebAPI.Dtos;
using WebAPI.Extensions;
using WebAPI.Features.Content.Commands.UpdateContent;
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

        Configure<UpdateContentDto, Unit>(
            group.MapPut("/{page}/{key}",
                async (UpdateContentDto dto, string page, string key, string? lang, IMediator mediator) =>
                {
                    await mediator.Send(new UpdateContentCommand(dto, page, key, lang ?? "en"));
                    return Results.NoContent();
                })
                .RequireScope("write:content"),
                "Update content",
                "Content",
                "Updates desired content"
            );
    }
}