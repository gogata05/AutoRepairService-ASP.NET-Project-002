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

        public DbSet<Car> Cars { get; set; }
        public DbSet<Offer> Offers { get; set; }

        //public DbSet<RepairOffer> JobsOffers { get; set; }
        protected override void OnModelCreating(ModelBuilder builder)
        {

            builder.ApplyConfiguration(new UserConfiguration());
            builder.ApplyConfiguration(new RoleConfiguration());
            builder.ApplyConfiguration(new UserRoleConfiguration());


            foreach (var relationship in builder.Model.GetEntityTypes().SelectMany(e => e.GetForeignKeys()))
            {
                relationship.DeleteBehavior = DeleteBehavior.Restrict;
            }

            builder.Entity<RepairOffer>()
                .HasKey(x => new { x.RepairId, x.OfferId });

            //builder.Entity<CarCart>()
            //    .HasKey(x => new { x.EquipmentId, x.CartId });

            base.OnModelCreating(builder);
        }
    }
}