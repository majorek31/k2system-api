using MediatR;
using WebAPI.Dtos;
using WebAPI.Features.Review.Commands.CreateReview;

namespace WebAPI.Endpoints.Review;

public class ReviewEndpoint : Endpoint
{
    public override void MapEndpoints(IEndpointRouteBuilder route)
    {
        var group = route.MapGroup("review");

        Configure<CreateReviewDto, Unit>(
            group.MapPost("/", async (IMediator mediator, CreateReviewDto dto) =>
            {
                var result = await mediator.Send(new CreateReviewCommand(dto));
                var location = $"/review/{result.Id}";
                return Results.Created(location, new());
            }),
            "CreateReview",
            "Review",
            "Creates a review"
        );
    }
}