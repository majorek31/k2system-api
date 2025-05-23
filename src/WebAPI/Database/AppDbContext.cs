using System.Text.Json;
using Microsoft.EntityFrameworkCore;
using WebAPI.Common;
using WebAPI.Entities;

namespace WebAPI.Database;

public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
{
    public DbSet<User> Users { get; set; }
    public DbSet<RefreshToken> RefreshTokens { get; set; }
    public DbSet<EditableContent> EditableContents { get; set; }
    public DbSet<Scope> Scopes { get; set; }
    public DbSet<Product> Products { get; set; }
    public DbSet<Order> Orders { get; set; }
    public DbSet<OrderItem> OrderItems { get; set; }
    public DbSet<ProductImage> ProductImages { get; set; }
    public DbSet<Review> Reviews { get; set; }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Scope>()
            .HasAlternateKey(s => s.Value);

        modelBuilder.Entity<Order>()
            .Property(o => o.OrderStatus)
            .HasConversion<string>();
        
        modelBuilder.Entity<OrderItem>()
            .HasOne(oi => oi.Product)
            .WithMany()
            .HasForeignKey(oi => oi.ProductId);
        modelBuilder.Entity<OrderItem>()
            .Navigation(oi => oi.Product)
            .AutoInclude();
        modelBuilder.Entity<OrderItem>()
            .Property(oi => oi.TotalPrice)
            .HasColumnType("decimal(18,2)");
        
        modelBuilder.Entity<OrderItem>()
            .Ignore(oi => oi.TotalPrice);

        modelBuilder.Entity<Review>()
            .Navigation(x => x.User)
            .AutoInclude();
        
        modelBuilder.Entity<User>()
            .HasDiscriminator<string>("UserType")
            .HasValue<UserPersonal>("Personal")
            .HasValue<UserCompany>("Company");
        
        modelBuilder.Entity<Product>()
            .Navigation(p => p.ProductImages)
            .AutoInclude();
        
        modelBuilder.Entity<Product>()
            .HasMany(p => p.ProductImages)
            .WithOne(pi => pi.Product)
            .HasForeignKey(pi => pi.ProductId);
        
        modelBuilder.Entity<User>()
            .HasMany(u => u.Scopes)
            .WithMany(s => s.Users)
            .UsingEntity(j => j.ToTable("UserScopes"));
        
        modelBuilder.Entity<User>()
            .Navigation(u => u.Scopes)
            .AutoInclude();
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