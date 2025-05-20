using FluentValidation;
using Mapster;
using MediatR;
using WebAPI.Repositories.ReviewRepository;
using WebAPI.Services;

namespace WebAPI.Features.Review.Commands.CreateReview;

public class CreateReviewCommandHandler(IReviewRepository reviewRepository, IAuthService authService) : IRequestHandler<CreateReviewCommand, Entities.Review>
{
    public async Task<Entities.Review> Handle(CreateReviewCommand request, CancellationToken cancellationToken)
    {
        var review = request.Dto.Adapt<Entities.Review>();
        var user = await authService.GetUser();
        if (user is null)
            throw new Exception("User validation failed");
        review.User = user;
        await reviewRepository.CreateAsync(review);
        return review;
    }
}