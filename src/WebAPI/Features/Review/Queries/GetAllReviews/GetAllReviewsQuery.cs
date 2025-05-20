using MediatR;
using WebAPI.Dtos;

public record GetAllReviewsQuery() : IRequest<IEnumerable<ReviewDto>>;