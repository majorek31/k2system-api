using Mapster;
using MediatR;
using WebAPI.Dtos;
using WebAPI.Repositories.MediaRepository;

namespace WebAPI.Features.Media.Queries.GetAllMedia;

class GetAllMediaQueryHandler(IMediaRepository mediaRepository) : IRequestHandler<GetAllMediaQuery, IEnumerable<MediaDto>>
{
    public async Task<IEnumerable<MediaDto>> Handle(GetAllMediaQuery request, CancellationToken cancellationToken)
    {
        var media = await mediaRepository.GetAsync();
        return media.Adapt<IEnumerable<MediaDto>>();
    }
}