using Mapster;
using MediatR;
using WebAPI.Entities;
using WebAPI.Repositories.EditableContentRepository;
using WebAPI.Services;

namespace WebAPI.Features.Content.Commands.UpdateContent;

public class UpdateContentCommandHandler(
    IEditableContentRepository editableContentRepository,
    IAuthService authService) : IRequestHandler<UpdateContentCommand, Unit>
{
    public async Task<Unit> Handle(UpdateContentCommand request, CancellationToken cancellationToken)
    {
        var user = await authService.GetUser();
        if (user is null)
            throw new Exception("User not authenticated");
        var editableContent = await editableContentRepository.GetEditableContentAsync(request.Page, request.Key, request.Language);
        if (editableContent is null)
            throw new Exception("Content not validated");
        editableContent.Content = request.Dto.Content;
        editableContent.LastEditor = user;
        await editableContentRepository.UpdateAsync(editableContent);
        return Unit.Value;
    }
}