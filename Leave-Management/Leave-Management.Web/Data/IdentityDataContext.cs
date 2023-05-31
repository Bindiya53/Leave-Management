using Leave_Management.Leave_Management.Leave_Management.Web.Configurations.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Leave_Management.Web.Data;

public class IdentityDataContext : IdentityDbContext<IdentityUser>
{
    public IdentityDataContext(DbContextOptions<IdentityDataContext> options)
        : base(options)
    {
    }

    public DbSet<LeaveType> LeaveTypes { get; set; }
    public DbSet<LeaveAllocation> LeaveAllocations { get; set; }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        builder.ApplyConfiguration(new RoleSeedConfiguration());
        builder.ApplyConfiguration(new UserSeedConfiguration());
        builder.ApplyConfiguration(new UserRoleSeedConfiguration());
        // Customize the ASP.NET Identity model and override the defaults if needed.
        // For example, you can rename the ASP.NET Identity table names and more.
        // Add your customizations after calling base.OnModelCreating(builder);
    }
}
