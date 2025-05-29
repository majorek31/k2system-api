using Mapster;
using MediatR;
using WebAPI.Dtos;
using WebAPI.Repositories.MediaRepository;

namespace WebAPI.Features.Media.Queries.GetMediaById;

public class GetMediaByIdQueryHandler(IMediaRepository mediaRepository) : IRequestHandler<GetMediaByIdQuery, MediaDto?>
{
    public async Task<MediaDto?> Handle(GetMediaByIdQuery request, CancellationToken cancellationToken)
    {
        var media = await mediaRepository.GetByIdAsync(request.MediaId);
        return media?.Adapt<MediaDto>();
    }
}