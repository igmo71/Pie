using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Pie.Admin.Api.Modules.Nginx;

namespace Pie.Admin.Api.Data
{
    public class AppDbContext : IdentityDbContext<AppUser>
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
            //Database.Migrate();
        }

        public DbSet<Tenant> Tenants { get; set; }
    }
}
