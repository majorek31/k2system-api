﻿using WebAPI.Entities;

namespace WebAPI.Repositories.RefreshTokenRepository;

public interface IRefreshTokenRepository : IRepository<RefreshToken>
{
    Task<RefreshToken?> GetRefreshTokenByTokenAsync(string token);
    Task<IEnumerable<RefreshToken>> GetRefreshTokensByUser(User user);
}