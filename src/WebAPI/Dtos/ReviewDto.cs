using WebAPI.Entities;

namespace WebAPI.Dtos;

public record ReviewDto(int Id, string Content, float Rating, UserDto User);