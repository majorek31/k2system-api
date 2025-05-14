using System.Text.Json;
using Microsoft.EntityFrameworkCore;
using WebAPI.Common;
using WebAPI.Entities;

namespace WebAPI.Database;

public class AppDbContext : DbContext
{
    public DbSet<User> Users { get; set; }
    public DbSet<RefreshToken> RefreshTokens { get; set; }
    public DbSet<EditableContent> EditableContents { get; set; }
    public DbSet<Scope> Scopes { get; set; }
    
    public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options)
    {}

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.Entity<User>()
            .HasMany(u => u.Scopes)
            .WithMany(s => s.Users)
            .UsingEntity(j => j.ToTable("UserScopes"));
    }
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