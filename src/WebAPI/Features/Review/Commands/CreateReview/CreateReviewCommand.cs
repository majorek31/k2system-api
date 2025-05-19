using MediatR;
using WebAPI.Dtos;

namespace WebAPI.Features.Review.Commands.CreateReview;

public record CreateReviewCommand(CreateReviewDto Dto) : IRequest<Entities.Review>;