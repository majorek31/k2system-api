using MediatR;
using WebAPI.Dtos;

namespace WebAPI.Features.Content.Queries.GetTranslationsForPage;

public record GetTranslationsForPageRequest(string Page, string Language) : IRequest<IEnumerable<ContentDto>>;
