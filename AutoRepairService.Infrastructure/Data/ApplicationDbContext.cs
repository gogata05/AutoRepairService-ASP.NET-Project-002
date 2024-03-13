using AutoRepairService.Infrastructure.Data.Configuration;
using AutoRepairService.Infrastructure.Data.EntityModels;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace AutoRepairService.Infrastructure.Data
{
    public class ApplicationDbContext : IdentityDbContext<User>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Repair> Repairs { get; set; }

        public DbSet<RepairCategory> RepairsCategories { get; set; }

        public DbSet<Car> Cars { get; set; }

        public DbSet<CarCategory> CarsCategories { get; set; }

        public DbSet<Offer> Offers { get; set; }

        public DbSet<RepairOffer> RepairOffer { get; set; }

        public DbSet<RepairStatus> RepairStatus { get; set; }

        public DbSet<Rating> Ratings { get; set; }

        public DbSet<Cart> Carts { get; set; }

        public DbSet<Order> Orders { get; set; }

        public DbSet<Message> Messages { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            //Remove comment to seed the DB(Comment to start Unit tests)

            builder.ApplyConfiguration(new UserConfiguration());
            builder.ApplyConfiguration(new RoleConfiguration());
            builder.ApplyConfiguration(new UserRoleConfiguration());
            builder.ApplyConfiguration(new RepairStatusConfiguration());
            builder.ApplyConfiguration(new RepairCategoryConfiguration());
            builder.ApplyConfiguration(new CarCategoryConfiguration());
            builder.ApplyConfiguration(new CarConfiguration());

            //Remove comment to seed the DB(Comment to start Unit tests)


            foreach (var relationship in builder.Model.GetEntityTypes().SelectMany(e => e.GetForeignKeys()))
            {
                relationship.DeleteBehavior = DeleteBehavior.Restrict;
            }

            builder.Entity<RepairOffer>()
                .HasKey(x => new { x.RepairId, x.OfferId });

            builder.Entity<CarCart>()
                .HasKey(x => new { x.CarId, x.CartId });

            base.OnModelCreating(builder);
        }
    }
}