using MediatR;
using WebAPI.Dtos;

namespace WebAPI.Features.Media.Commands;

public record UploadFileCommand(IFormFile File) : IRequest<Entities.Media>;