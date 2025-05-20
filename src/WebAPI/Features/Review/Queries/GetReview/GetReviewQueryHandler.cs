using Mapster;
using MediatR;
using WebAPI.Dtos;
using WebAPI.Repositories.ReviewRepository;

namespace WebAPI.Features.Review.Queries.GetReview;

public class GetReviewQueryHandler(IReviewRepository reviewRepository) : IRequestHandler<GetReviewQuery, ReviewDto?>
{
    public async Task<ReviewDto?> Handle(GetReviewQuery request, CancellationToken cancellationToken)
    {
        var review = await reviewRepository.GetByIdAsync(request.Id);
        return review.Adapt<ReviewDto>();
    }
}