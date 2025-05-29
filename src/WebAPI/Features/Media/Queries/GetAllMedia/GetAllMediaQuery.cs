using MediatR;
using WebAPI.Dtos;

namespace WebAPI.Features.Media.Queries.GetAllMedia;

public record GetAllMediaQuery() : IRequest<IEnumerable<MediaDto>>;