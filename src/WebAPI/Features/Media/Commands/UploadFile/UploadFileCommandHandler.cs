using MediatR;
using WebAPI.Repositories.MediaRepository;
using WebAPI.Services;

namespace WebAPI.Features.Media.Commands;

public class UploadFileCommandHandler(IMediaRepository mediaRepository, IAuthService authService) : IRequestHandler<UploadFileCommand, Entities.Media>
{
    private const string UPLOAD_FOLDER = "wwwroot/Uploads";
    public async Task<Entities.Media> Handle(UploadFileCommand request, CancellationToken cancellationToken)
    {
        var user = await authService.GetUser();
        if (user == null)
            throw new Exception("User not authenticated");
        var media = new Entities.Media
        {
            FileName = request.File.FileName,
            MimeType = request.File.ContentType,
            FileSize = request.File.Length,
            UploaderId = user.Id,
            Path = UPLOAD_FOLDER,
        };
        
        if (!Directory.Exists(UPLOAD_FOLDER))
        {
            Directory.CreateDirectory(UPLOAD_FOLDER);
        }
        await mediaRepository.CreateAsync(media);

        var fileName = media.Id + "-" + request.File.FileName;
        var filePath = Path.Combine(UPLOAD_FOLDER, fileName);
        media.Path = Path.Combine("Uploads", fileName);

        await using var stream = new FileStream(filePath, FileMode.Create);
        await request.File.CopyToAsync(stream, cancellationToken);
        await mediaRepository.UpdateAsync(media);
        return media;
    }
}