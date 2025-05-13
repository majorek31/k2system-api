using Microsoft.EntityFrameworkCore;
using WebAPI.Common;
using WebAPI.Entities;

namespace WebAPI.Database;

public class AppDbContext : DbContext
{
    public DbSet<User> Users { get; set; }
    public DbSet<RefreshToken> RefreshTokens { get; set; }
    public DbSet<EditableContent> EditableContents { get; set; }
    
    public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options)
    {}

    public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken())
    {
        foreach (var entityEntry in ChangeTracker.Entries<BaseEntity>()
                     .Where(x => x.State is EntityState.Added or EntityState.Modified))
        {
            var utcNow = DateTime.UtcNow;
            entityEntry.Entity.UpdatedAt = utcNow;
            if (entityEntry.State is EntityState.Added)
            {
                entityEntry.Entity.CreatedAt = utcNow;
            }
        }
        return base.SaveChangesAsync(cancellationToken);
    }
}