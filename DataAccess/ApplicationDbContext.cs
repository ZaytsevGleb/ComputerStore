using DataAccess.Entities;
using Microsoft.EntityFrameworkCore;

namespace DataAccess;

public sealed class ApplicationDbContext : DbContext
{
    public DbSet<Product> Products { get; set; }

    public ApplicationDbContext(DbContextOptions options) : base(options)
    {
    }
}
