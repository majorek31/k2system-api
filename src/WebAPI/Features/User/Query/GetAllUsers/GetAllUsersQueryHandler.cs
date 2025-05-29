using Mapster;
using MediatR;
using WebAPI.Dtos;
using WebAPI.Entities;
using WebAPI.Repositories.UserRepository;

namespace WebAPI.Features.User.Query.GetAllUsers;

public class GetAllUsersQueryHandler(IUserRepository userRepository) : IRequestHandler<GetAllUsersQuery, IEnumerable<UserDto>>
{
    public async Task<IEnumerable<UserDto>> Handle(GetAllUsersQuery request, CancellationToken cancellationToken)
    {
        var users = await userRepository.GetAsync(); 
        
        var userDtos = users.Select(user =>
        {
            return user switch
            {
                UserCompany company => new UserDto(
                    Id: company.Id,
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
                    Id: personal.Id,
                    Email: personal.Email,
                    UserType: "Personal",
                    FirstName: personal.FirstName,
                    LastName: personal.LastName,
                    CreatedAt: personal.CreatedAt,
                    Scopes: personal.Scopes.Adapt<ICollection<ScopeDto>>(),
                    VATNumber: null,
                    CompanyName: null
                ),

                _ => throw new InvalidOperationException("Unknown user type")
            };
        });

        return userDtos;
    }

}