namespace WebAPI.Dtos;

public record MediaDto(int Id, string FileName, long FileSize, string MimeType, int UploaderId, string Path);