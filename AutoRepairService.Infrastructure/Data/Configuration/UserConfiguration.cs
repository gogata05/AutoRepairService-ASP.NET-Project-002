using AutoRepairService.Infrastructure.Data.EntityModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AutoRepairService.Infrastructure.Data.Configuration
{
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.HasData(AddUsers());
        }

        private List<User> AddUsers()
        {
            var users = new List<User>();
            var hasher = new PasswordHasher<User>();

            var user = new User()
            {
                Id = "dea12856-c198-4129-b3f3-b893d8395082",
                UserName = "mechanic",
                NormalizedUserName = "MECHANIC",
                Email = "mechanic@mail.com",
                NormalizedEmail = "MECHANIC@MAIL.COM"
            };

            user.PasswordHash = hasher.HashPassword(user, "mechanic");
            users.Add(user);

            user = new User()
            {
                Id = "6d5800ce-d726-4fc8-83d9-d6b3ac1f591e",
                UserName = "customer",
                NormalizedUserName = "CUSTOMER",
                Email = "customer@mail.com",
                NormalizedEmail = "CUSTOMER@MAIL.COM"
            };

            user.PasswordHash =
            hasher.HashPassword(user, "customer");

            users.Add(user);

            user = new User()
            {
                Id = "d6b3ac1f-4fc8-d726-83d9-6d5800ce591e",
                UserName = "admin",
                NormalizedUserName = "ADMIN",
                Email = "admin@mail.com",
                NormalizedEmail = "ADMIN@MAIL.COM"
            };

            user.PasswordHash =
            hasher.HashPassword(user, "admin");

            users.Add(user);

            return users;
        }
    }
}

