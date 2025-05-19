using WebAPI.Common;

namespace WebAPI.Entities;

public class Review : BaseEntity
{
    public string Content { get; set; }
    public float Rating { get; set; }
    
    public int UserId { get; set; }
    public User User { get; set; }
}