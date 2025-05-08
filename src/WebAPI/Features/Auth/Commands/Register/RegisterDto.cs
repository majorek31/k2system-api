namespace WebAPI.Features.Auth.Commands;

public record RegisterDto(string Email, string FirstName, string LastName, string Password);