using WebAPI.Entities;

namespace WebAPI.Database;

public static class DatabaseSeeder
{
    public static async void Seed(IServiceProvider serviceProvider)
    {
        var context = serviceProvider.CreateScope().ServiceProvider.GetRequiredService<AppDbContext>();
        await context.Database.EnsureCreatedAsync();
        if (!context.Scopes.Any())
        {
            var scopes = new List<Scope>
            {
                new Scope { Value = "admin" },
                new Scope { Value = "write:content" },
                new Scope { Value = "read:user" }
            };
            await context.Scopes.AddRangeAsync(scopes);
            await context.SaveChangesAsync();
        }

        if (!context.Users.Any())
        {
            var user = new User
            {
                Email = "admin@k2systems.pl",
                FirstName = "admin",
                LastName = "admin",
                PasswordHash = "$2a$11$LZCEiWBMqqPxpuP4TzG07OjYw97XMLcgGdHsShSZZW5jvU3uERxMO",
                Scopes = context.Scopes.ToList()
            };
            await context.Users.AddAsync(user);
            await context.SaveChangesAsync();
        }

        if (!context.EditableContents.Any())
        {
            var admin = context.Users.First();
            var contents = new List<EditableContent>
            {
                new EditableContent
                {
                    LastEditor = admin,
                    Content = "Welcome on our website",
                    Key = "WelcomeHeader",
                    Page = "HomePage",
                    Language = "en"
                }
            };
            await context.EditableContents.AddRangeAsync(contents);
            await context.SaveChangesAsync();
        }
    }
}