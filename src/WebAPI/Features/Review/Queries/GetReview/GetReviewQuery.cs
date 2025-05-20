using MediatR;
using WebAPI.Dtos;

namespace WebAPI.Features.Review.Queries.GetReview;

public record GetReviewQuery(int Id) : IRequest<ReviewDto?>;