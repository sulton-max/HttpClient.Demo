using Microsoft.EntityFrameworkCore;
using N2.DelegateHandlers.Api.DataContexts;
using N2.DelegateHandlers.Api.Models;

namespace N2.DelegateHandlers.Api.Data;

/// <summary>
/// Extension methods for initializing seed data in the application.
/// </summary>
public static class SeedDataExtensions
{
    /// <summary>
    /// Initializes seed data in the AppDbContext by checking for existing users and seeding them if necessary.
    /// </summary>
    /// <param name="serviceProvider">The service provider to resolve dependencies.</param>
    /// <returns>An asynchronous task representing the initialization process.</returns>
    public static async ValueTask InitializeSeedAsync(this IServiceProvider serviceProvider)
    {
        var appDbContext = serviceProvider.GetRequiredService<AppDbContext>();

        if (!await appDbContext.Users.AnyAsync())
            await appDbContext.SeedUsersAsync();
    }

    /// <summary>
    /// Seeds user data into the AppDbContext using Bogus library.
    /// </summary>
    /// <param name="dbContext">The AppDbContext instance to seed data into.</param>
    /// <returns>An asynchronous task representing the seeding process.</returns>
    private static async ValueTask SeedUsersAsync(this AppDbContext dbContext)
    {
        dbContext.Users.AddRange(
            new List<User>
            {
                new()
                {
                    Id = Guid.Parse("31266277-457F-4E50-8195-4B43D55020C7"),
                    EmailAddress = "john.doe@gmail.com",
                    FirstName = "John",
                    LastName = "Doe",
                    Password = "JohnDoe_1",
                    RoleType = RoleType.User
                }
            }
        );

        await dbContext.SaveChangesAsync();
    }
}