using DataAccess.Entities;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.Context;

public sealed class ApplicationDbContext : DbContext
{
    public DbSet<Product> Products { get; set; }

    public ApplicationDbContext(DbContextOptions options) : base(options)
    {
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.AddInterceptors(new SetupDateInterceptor());
    }
}