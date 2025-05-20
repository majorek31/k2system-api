using Mapster;
using MediatR;
using WebAPI.Dtos;
using WebAPI.Repositories.ReviewRepository;

namespace WebAPI.Features.Review.Queries.GetAllReviews;

public class GetAllReviewsQueryHandler(IReviewRepository reviewRepository) : IRequestHandler<GetAllReviewsQuery, IEnumerable<ReviewDto>>
{
    public async Task<IEnumerable<ReviewDto>> Handle(GetAllReviewsQuery request, CancellationToken cancellationToken)
    {
        var reviews = await reviewRepository.GetAsync();
        return reviews.Adapt<IEnumerable<ReviewDto>>();
    }
}