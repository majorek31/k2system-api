using WebAPI.Common;

namespace WebAPI.Entities;

public class User : BaseEntity
{
    public required string Email { get; set; }
    public required string FirstName { get; set; }
    public required string LastName { get; set; }
    public required string PasswordHash { get; set; }
}