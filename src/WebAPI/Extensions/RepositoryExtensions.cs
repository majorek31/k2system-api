﻿using WebAPI.Repositories.EditableContentRepository;
using WebAPI.Repositories.MediaRepository;
using WebAPI.Repositories.OrderRepository;
using WebAPI.Repositories.ProductRepository;
using WebAPI.Repositories.RefreshTokenRepository;
using WebAPI.Repositories.ReviewRepository;
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
        
        services.AddScoped<IProductRepository, ProductRepository>();
        services.AddScoped<IReviewRepository, ReviewRepository>();
        
        services.AddScoped<IOrderRepository, OrderRepository>();
        services.AddScoped<IMediaRepository, MediaRepository>();
        return services;
    }
}