namespace WebAPI.Dtos;

public record UpdateUserScopesDto(ICollection<ScopeDto> Scopes);