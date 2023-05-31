using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Leave_Management.Leave_Management.Leave_Management.Web.Configurations.Entities
{
    public class UserRoleSeedConfiguration : IEntityTypeConfiguration<IdentityUserRole<string>>
    {

        public void Configure(EntityTypeBuilder<IdentityUserRole<string>> builder)
        {
             builder.HasData(
                new IdentityUserRole<string>{
                    RoleId = "0e335630-5594-4d7c-90b7-9251eea7d874",
                    UserId = "0e995630-3594-4d7c-90a7-9251eeb7d874"
                },
                new IdentityUserRole<string>{
                    RoleId = "0f775630-6694-5d7c-90g7-9251jja7d874",
                    UserId = "0f775630-3694-4f7c-90d7-9251ffb7d874"
                }
            );
        }
    }
}

