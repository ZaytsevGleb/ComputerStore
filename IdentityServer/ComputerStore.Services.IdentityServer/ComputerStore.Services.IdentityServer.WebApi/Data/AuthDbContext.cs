using ComputerStore.Services.IdentityServer.WebApi.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace ComputerStore.Services.IdentityServer.WebApi.Data
{
    public class AuthDbContext : IdentityDbContext<User, Role, Guid>
    {
        public AuthDbContext(DbContextOptions<AuthDbContext> options) : base(options)
        {
            if (Database.IsNpgsql())
            {
                Database.Migrate();
            }
        }
    }
}
