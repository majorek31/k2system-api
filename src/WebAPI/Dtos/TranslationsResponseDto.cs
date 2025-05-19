namespace WebAPI.Dtos;

public record ContentDto(int Id, string Page, string Key, string Content);
public record TranslationsResponseDto(IEnumerable<ContentDto> Translations);