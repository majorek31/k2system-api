using MediatR;
using WebAPI.Dtos;

namespace WebAPI.Features.Media.Queries.GetMediaById;

public record GetMediaByIdQuery(int MediaId) : IRequest<MediaDto?>;