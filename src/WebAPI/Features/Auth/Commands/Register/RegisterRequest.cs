namespace WebAPI.Features.Auth.Commands;

public record RegisterRequest(string Email, string FirstName, string LastName, string Password);