using Mapster;
using MediatR;
using WebAPI.Dtos;
using WebAPI.Repositories.EditableContentRepository;

namespace WebAPI.Features.Content.Queries.GetTranslationsForPage;

public class GetTranslationsForPageRequestHandler(IEditableContentRepository contentRepository) : IRequestHandler<GetTranslationsForPageRequest, IEnumerable<ContentDto>>
{
    public async Task<IEnumerable<ContentDto>> Handle(GetTranslationsForPageRequest request, CancellationToken cancellationToken)
    {
        var content = await contentRepository.GetEditableContentAsync(request.Page, request.Language);
        return content.Adapt<IEnumerable<ContentDto>>();
    }
}