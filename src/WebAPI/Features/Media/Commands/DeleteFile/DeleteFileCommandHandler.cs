using MediatR;
using WebAPI.Common.Exceptions;
using WebAPI.Repositories.MediaRepository;

namespace WebAPI.Features.Media.Commands.DeleteFile;

public class DeleteFileCommandHandler(IMediaRepository mediaRepository) : IRequestHandler<DeleteFileCommand, Unit>
{
    public async Task<Unit> Handle(DeleteFileCommand request, CancellationToken cancellationToken)
    {
        var media = await mediaRepository.GetByIdAsync(request.MediaId);
        if (media == null)
            throw new NotFoundException();
        var filePath = Path.Combine("wwwroot", media.Path);
        if (File.Exists(filePath))
            File.Delete(filePath);
        await mediaRepository.DeleteAsync(media);
        return Unit.Value;
    }
}