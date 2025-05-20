using MediatR;
using WebAPI.Dtos;
using WebAPI.Extensions;
using WebAPI.Features.Review.Commands.CreateReview;
using WebAPI.Features.Review.Queries.GetReview;

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
            })
            .RequireScope("write:review"),
            "CreateReview",
            "Review",
            "Creates a review"
        );
        
        Configure<IEnumerable<ReviewDto>>(
        group.MapGet("/", async (IMediator mediator) =>
            {
                var result = await mediator.Send(new GetAllReviewsQuery());
                return Results.Ok(result);
            }),
            "GetAllReviews",
            "Review",
            "Returns all reviews"
        );

        Configure<IEnumerable<ReviewDto>>(
            group.MapGet("/{reviewId:int}", async (IMediator mediator, int reviewId) =>
            {
                var result = await mediator.Send(new GetReviewQuery(reviewId));
                return result is null ? Results.NotFound() : Results.Ok(result);
            }),
            "GetReviewById",
            "Review",
            "Returns a review"
        );
    }
}