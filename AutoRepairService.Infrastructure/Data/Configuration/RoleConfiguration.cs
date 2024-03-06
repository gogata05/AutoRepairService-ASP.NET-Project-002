using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
namespace AutoRepairService.Infrastructure.Data.Configuration
{
    public class RoleConfiguration : IEntityTypeConfiguration<IdentityRole>
    {
        public void Configure(EntityTypeBuilder<IdentityRole> builder)
        {
            builder.HasData(CreateRoles());
        }

        private List<IdentityRole> CreateRoles()
        {
            var roles = new List<IdentityRole>();

            var role = new IdentityRole()
            {
                Id = "1e62f853-4a41-4652-b9a9-8e8b236e24c7",
                Name = "Administrator",
                NormalizedName = "ADMINISTRATOR",
                ConcurrencyStamp = "1fa54d33-a8ba-47b1-be64-02d6d4836152"
            };

            roles.Add(role);

            role = new IdentityRole()
            {
                Id = "5d937746-9833-4886-83d1-3c125ad5294c",
                Name = "Customer",
                NormalizedName = "CUSTOMER",
                ConcurrencyStamp = "80196750-3beb-42a1-b321-0aa9c9f41275"
            };

            roles.Add(role);

            role = new IdentityRole()
            {
                Id = "c8a8cf93-46b1-4e79-871a-1f4742a0db83",
                Name = "Mechanic",
                NormalizedName = "MECHANIC",
                ConcurrencyStamp = "292f8d0e-c368-4cf1-bb0a-b8cda6096ea9"
            };

            roles.Add(role);

            return roles;
        }
    }
}
