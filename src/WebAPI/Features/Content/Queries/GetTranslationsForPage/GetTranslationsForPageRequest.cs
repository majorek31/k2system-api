using MediatR;

namespace WebAPI.Features.Content.Queries.GetTranslationsForPage;

public record GetTranslationsForPageRequest(string Page, string Language) : IRequest<TranslationsResponseDto>;
