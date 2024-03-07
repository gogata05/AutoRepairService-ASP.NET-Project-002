using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AutoRepairService.Infrastructure.Data.Configuration
{
    public class UserRoleConfiguration : IEntityTypeConfiguration<IdentityUserRole<string>>
    {
        public void Configure(EntityTypeBuilder<IdentityUserRole<string>> builder)
        {
            builder.HasData(UserToRoles());
        }

        private List<IdentityUserRole<string>> UserToRoles()
        {
            var userRoles = new List<IdentityUserRole<string>>();
            //admin
            var userRole = new IdentityUserRole<string>()
            {
                RoleId = "1e62f853-4a41-4652-b9a9-8e8b236e24c7",
                UserId = "d6b3ac1f-4fc8-d726-83d9-6d5800ce591e"
            };

            userRoles.Add(userRole);
            //customer
            userRole = new IdentityUserRole<string>()
            {
                RoleId = "5d937746-9833-4886-83d1-3c125ad5294c",
                UserId = "6d5800ce-d726-4fc8-83d9-d6b3ac1f591e"
            };

            userRoles.Add(userRole);
            //Mechanic
            userRole = new IdentityUserRole<string>()
            {
                RoleId = "c8a8cf93-46b1-4e79-871a-1f4742a0db83",
                UserId = "dea12856-c198-4129-b3f3-b893d8395082"
            };

            userRoles.Add(userRole);

            return userRoles;
        }
    }
}
