using WebAPI.Entities;

namespace WebAPI.Dtos;

public record UserDto(int Id,
    string Email,
    string UserType,
    string FirstName,
    string LastName,
    DateTime CreatedAt,
    ICollection<ScopeDto> Scopes,
    string? VATNumber,
    string? CompanyName);