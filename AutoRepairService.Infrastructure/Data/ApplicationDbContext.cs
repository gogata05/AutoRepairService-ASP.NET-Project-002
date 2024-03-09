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

        //public DbSet<RepairOffer> RepairsOffers { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {

            builder.ApplyConfiguration(new UserConfiguration());
            builder.ApplyConfiguration(new RoleConfiguration());
            builder.ApplyConfiguration(new UserRoleConfiguration());
            //builder.ApplyConfiguration(new RepairCategoryConfiguration());
            //builder.ApplyConfiguration(new RepairStatusConfiguration());


            builder.Entity<RepairOffer>()
                .HasKey(x => new { x.RepairId, x.OfferId });

            builder.Entity<Offer>().HasMany(a => a.Owner).WithOne().OnDelete(DeleteBehavior.Restrict);

            base.OnModelCreating(builder);
        }
    }
}