using FluentValidation;
using WebAPI.Dtos;

namespace WebAPI.Features.Review.Commands.CreateReview;

public class CreateReviewCommandValidator : AbstractValidator<CreateReviewDto>
{
    public CreateReviewCommandValidator()
    {
        RuleFor(x => x.Content).NotEmpty().WithMessage("Content is required");
        RuleFor(x => x.Rating)
            .NotEmpty().WithMessage("Rating is required")
            .GreaterThan(0).WithMessage("Rating must be greater than 0")
            .LessThanOrEqualTo(5).WithMessage("Rating must be less than or equal to 5");
    }
}