using Mapster;
using MediatR;
using WebAPI.Repositories.EditableContentRepository;

namespace WebAPI.Features.Content.Queries.GetTranslationsForPage;

public class GetTranslationsForPageRequestHandler(IEditableContentRepository contentRepository) : IRequestHandler<GetTranslationsForPageRequest, TranslationsResponseDto>
{
    public async Task<TranslationsResponseDto> Handle(GetTranslationsForPageRequest request, CancellationToken cancellationToken)
    {
        var content = await contentRepository.GetEditableContentAsync(request.Page, request.Language);
        var result = content
            .Select(x => x.Adapt<ContentDto>());
        return new TranslationsResponseDto(result);
    }
}