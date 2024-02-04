using Microsoft.EntityFrameworkCore;
using N2.DelegateHandlers.Api.Models;

namespace N2.DelegateHandlers.Api.DataContexts;

public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
{
    public DbSet<User> Users => Set<User>();
}