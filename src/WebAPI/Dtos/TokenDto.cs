namespace WebAPI.Dtos;

public record TokenDto(int Id, string UserAgent, DateTime ExpiresAt, DateTime CreatedAt);