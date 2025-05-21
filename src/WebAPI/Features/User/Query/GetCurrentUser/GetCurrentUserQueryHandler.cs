using Mapster;
using MediatR;
using WebAPI.Dtos;
using WebAPI.Entities;
using WebAPI.Services;

namespace WebAPI.Features.User.Query.GetCurrentUser;

public class GetCurrentUserQueryHandler(IAuthService authService) : IRequestHandler<GetCurrentUserQuery, UserDto>
{
    public async Task<UserDto> Handle(GetCurrentUserQuery request, CancellationToken cancellationToken)
    {
        var user = await authService.GetUser();
        if (user is null)
            throw new Exception("User not found");
        return user switch
        {
            UserCompany company => new UserDto(
                Email: company.Email,
                UserType: "Company",
                FirstName: company.FirstName,
                LastName: company.LastName,
                CreatedAt: company.CreatedAt,
                Scopes: company.Scopes.Adapt<ICollection<ScopeDto>>(),
                VATNumber: company.VATNumber,
                CompanyName: company.CompanyName
            ),

            UserPersonal personal => new UserDto(
                Email: personal.Email,
                UserType: "Personal",
                FirstName: personal.FirstName,
                LastName: personal.LastName,
                CreatedAt: personal.CreatedAt,
                Scopes: personal.Scopes.Adapt<ICollection<ScopeDto>>(),
                VATNumber: null,
                CompanyName: null
            ),

            _ => throw new Exception("User not found")
        };
    }
}