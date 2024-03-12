using AutoRepairService.Infrastructure.Data.EntityModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AutoRepairService.Infrastructure.Data.Configuration
{
    internal class RepairCategoryConfiguration : IEntityTypeConfiguration<RepairCategory>
    {
        public void Configure(EntityTypeBuilder<RepairCategory> builder)
        {
            builder.HasData(CreateCategories());
        }

        private List<RepairCategory> CreateCategories()
        {
            List<RepairCategory> categories = new List<RepairCategory>()
            {
                new RepairCategory()
                {
                    Id = 1,
                    Name = "General Maintenance"
                },

                new RepairCategory()
                {
                    Id = 2,
                    Name = "Engine and Transmission"
                },

                new RepairCategory()
                {
                    Id = 3,
                    Name = "Electrical Systems"
                },

                new RepairCategory()
                {
                    Id = 4,
                    Name = "Suspension and Brakes"
                },

                new RepairCategory()
                {
                    Id = 5,
                    Name = "Bodywork"
                },

                new RepairCategory()
                {
                    Id = 6,
                    Name = "Exhaust Systems"
                },

                new RepairCategory()
                {
                    Id = 7,
                    Name = "Tires and Alignment"
                },

                new RepairCategory()
                {
                    Id = 8,
                    Name = "Other"
                }
            };

            return categories;
        }
    }
}