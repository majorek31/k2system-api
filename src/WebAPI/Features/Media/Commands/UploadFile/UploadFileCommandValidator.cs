using FluentValidation;
using WebAPI.Dtos;

namespace WebAPI.Features.Media.Commands;

public class UploadFileCommandValidator : AbstractValidator<UploadFileDto>
{
    public UploadFileCommandValidator()
    {
        long maxfileSize = 1 << 30;
        var allowedExtensions = new List<string> { ".jpg", ".jpeg", ".png", ".webp", ".pdf" };
        RuleFor(x => x.File)
            .Must(file => file.Length <= maxfileSize)
            .WithMessage("File is too big");
        RuleFor(x => x.File)
            .Must(file => allowedExtensions.Contains(Path.GetExtension(file.FileName).ToLower()))
            .WithMessage("File is not a valid extension");
    }
}