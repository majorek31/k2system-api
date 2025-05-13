namespace WebAPI.Features.Content.Queries.GetTranslationsForPage;

public record ContentDto(int Id, string Key, string Content);
public record TranslationsResponseDto(IEnumerable<ContentDto> Translations);