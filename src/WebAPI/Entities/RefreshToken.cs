﻿using WebAPI.Common;

namespace WebAPI.Entities;

public class RefreshToken : BaseEntity
{
    public required string Token { get; set; }
    
    public required string UserAgent { get; set; }
    public DateTime ExpiresAt { get; set; }
    public bool IsRevoked { get; set; }
    public int UserId { get; set; }
    public User User { get; set; }
}