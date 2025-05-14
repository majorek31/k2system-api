using WebAPI.Common;

namespace WebAPI.Entities;

public class Scope : BaseEntity
{
    public required string Value { get; set; } = string.Empty;
    
    public List<User> Users { get; set; } = new();
}