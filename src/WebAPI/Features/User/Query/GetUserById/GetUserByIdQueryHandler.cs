using Mapster;
using MediatR;
using WebAPI.Dtos;
using WebAPI.Repositories.UserRepository;

namespace WebAPI.Features.User.Query.GetUserById;

public class GetUserByIdQueryHandler(IUserRepository userRepository) : IRequestHandler<GetUserByIdQuery, UserDto?>
{
    public async Task<UserDto?> Handle(GetUserByIdQuery request, CancellationToken cancellationToken)
    {
        var user = await userRepository.GetByIdAsync(request.UserId);
        return user?.Adapt<UserDto>();
    }
}