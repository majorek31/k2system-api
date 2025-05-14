using WebAPI.Repositories.EditableContentRepository;
using WebAPI.Repositories.RefreshTokenRepository;
using WebAPI.Repositories.ScopeRepository;
using WebAPI.Repositories.UserRepository;

namespace WebAPI.Extensions;

public static class RepositoryExtensions
{
    public static IServiceCollection AddRepositories(this IServiceCollection services)
    {
        services.AddScoped<IUserRepository, UserRepository>();
        services.AddScoped<IRefreshTokenRepository, RefreshTokenRepository>();
        services.AddScoped<IEditableContentRepository, EditableContentRepository>();
        services.AddScoped<IScopeRepository, ScopeRepository>();
        return services;
    }
}