using AutoRepairService.Infrastructure.Data.EntityModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AutoRepairService.Infrastructure.Data.Configuration
{
    internal class CarConfiguration : IEntityTypeConfiguration<Car>
    {
        public void Configure(EntityTypeBuilder<Car> builder)
        {
            builder.HasData(CreateCategories());
        }

        private List<Car> CreateCategories()
        {
            List<Car> cars = new List<Car>()
            {
                new Car()
                {
                    Id = 1,
                    ModelOfCar = "BMW M2",
                    Brand = "BMW",
                    Mileage = 30000,
                    Year = 2010,
                    CarCategoryId = 2,
                    Description = "Best BMW",
                    OwnerId = "d6b3ac1f-4fc8-d726-83d9-6d5800ce591e",
                    IsActive = true,
                    Price = 2500.00m,
                    ImageUrl = "https://cdn.images.autoexposure.co.uk/AETA35653/AETV53606122_1.jpg"
                },




             };

            return cars;
        }
    }
}
