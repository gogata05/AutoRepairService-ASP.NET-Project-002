using AutoRepairService.Infrastructure.Data.EntityModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AutoRepairService.Infrastructure.Data.Configuration
{
    internal class CarCategoryConfiguration : IEntityTypeConfiguration<CarCategory>
    {
        public void Configure(EntityTypeBuilder<CarCategory> builder)
        {
            builder.HasData(CreateCategories());
        }

        private List<CarCategory> CreateCategories()
        {
            List<CarCategory> categories = new List<CarCategory>()
            {
                new CarCategory()
                {
                    Id = 1,
                    Name = "Electric and Hybrid"
                },

                new CarCategory()
                {
                    Id = 2,
                    Name = "Sport"
                },

                new CarCategory()
                {
                    Id = 3,
                    Name = "Sedan"
                },

                new CarCategory()
                {
                    Id = 4,
                    Name = "Coupe"
                },

                new CarCategory()
                {
                    Id = 5,
                    Name = "Minivan"
                },

                new CarCategory()
                {
                    Id = 6,
                    Name = "SUV"
                },

                new CarCategory()
                {
                    Id = 7,
                    Name = "Crossover"
                },

                new CarCategory()
                {
                    Id = 8,
                    Name = "Hatchback"
                },

             };

            return categories;
        }
    }
}
