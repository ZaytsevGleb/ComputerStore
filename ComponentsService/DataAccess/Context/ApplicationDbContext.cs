using ComputerStore.Services.CS.DataAccess.Entities;
using ComputerStore.Services.CS.DataAccess.Infrastructure;
using Microsoft.EntityFrameworkCore;

namespace ComputerStore.Services.CS.DataAccess.Context;

public sealed class ApplicationDbContext : DbContext
{
    public DbSet<Product> Products { get; set; }

    public ApplicationDbContext(DbContextOptions options) : base(options)
    {
        if (Database.IsNpgsql())
        {
            Database.Migrate();
        }
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.AddInterceptors(new SetupDateInterceptor());
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        if (Database.IsNpgsql())
            modelBuilder.ApplySeedData();
    }
}