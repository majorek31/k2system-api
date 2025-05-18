using WebAPI.Common;

namespace WebAPI.Entities;

public class User : BaseEntity
{
    public required string Email { get; set; }
    public required string FirstName { get; set; }
    public required string LastName { get; set; }
    public required string PasswordHash { get; set; }
    public List<Scope> Scopes { get; set;} = new();
}

public class UserPersonal : User
{
    public DateTime BirthDay { get; set; }
}

public class UserCompany : User
{
    public required string CompanyName { get; set; }
    public string? VATNumber { get; set; }
}