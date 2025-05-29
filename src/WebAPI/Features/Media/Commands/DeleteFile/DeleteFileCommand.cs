using MediatR;

namespace WebAPI.Features.Media.Commands.DeleteFile;

public record DeleteFileCommand(int MediaId) : IRequest<Unit>;