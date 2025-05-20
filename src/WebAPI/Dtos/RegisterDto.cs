namespace WebAPI.Dtos;

public record RegisterDto(
    string Email,
    string FirstName,
    string LastName,
    string Password,
    string UserType,
    string? VATNumber,
    string? CompanyName
);