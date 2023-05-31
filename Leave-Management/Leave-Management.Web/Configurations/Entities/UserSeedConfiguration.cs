using Leave_Management.Web.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Leave_Management.Leave_Management.Leave_Management.Web.Configurations.Entities
{
    public class UserSeedConfiguration : IEntityTypeConfiguration<Employee>
    {
        public void Configure(EntityTypeBuilder<Employee> builder)
        {
            var hasher = new PasswordHasher<Employee>();
            builder.HasData(
                new Employee{
                    Id = "0e995630-3594-4d7c-90a7-9251eeb7d874",
                    Email = "admin@rivet.com",
                    NormalizedEmail = "ADMIN@RIVET.COM",
                    NormalizedUserName = "ADMIN@RIVET.COM",
                    UserName = "admin@rivet.com",
                    FirstName = "System",
                    LastName = "Admin",
                    PasswordHash = hasher.HashPassword(null,"Admin@123"),
                    TaxId = "test",
                    EmailConfirmed = true
                },
                new Employee{
                    Id = "0f775630-3694-4f7c-90d7-9251ffb7d874",
                    Email = "user@rivet.com",
                    NormalizedEmail = "USER@RIVET.COM",
                    NormalizedUserName = "USER@RIVET.COM",
                    UserName = "user@rivet.com",
                    FirstName = "System",
                    LastName = "User",
                    PasswordHash = hasher.HashPassword(null,"User@123"),
                    TaxId = "test",
                    EmailConfirmed = true
                }
            );
        }
    }
}