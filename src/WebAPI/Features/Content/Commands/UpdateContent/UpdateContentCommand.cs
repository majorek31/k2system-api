using MediatR;
using WebAPI.Dtos;

namespace WebAPI.Features.Content.Commands.UpdateContent;

public record UpdateContentCommand(UpdateContentDto Dto,
    string Page,
    string Key,
    string Language) : IRequest<Unit>;