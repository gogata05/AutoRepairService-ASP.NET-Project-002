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
                    Mileage = 11000,
                    Year = 2019,
                    CarCategoryId = 2,
                    Description = "Best BMW",
                    OwnerId = "d6b3ac1f-4fc8-d726-83d9-6d5800ce591e",
                    IsActive = true,
                    Price = 104000.00m,
                    ImageUrl = "https://cdn.images.autoexposure.co.uk/AETA35653/AETV53606122_1.jpg"
                },
                new Car()
                {
                    Id = 2,
                    ModelOfCar = "Audi A8 50 TDI Quattro",
                    Brand = "Audi",
                    Mileage = 3000,
                    Year = 2018,
                    CarCategoryId = 2,
                    Description = "The perfect car for your needs",
                    OwnerId = "d6b3ac1f-4fc8-d726-83d9-6d5800ce591e",
                    IsActive = true,
                    Price = 80000.00m,
                    ImageUrl = "https://g1-bg.cars.bg/2023-10-26_2/653a77ef6cc86d6a120fd702o.jpg"
                },
                new Car()
                {
                    Id = 3,
                    ModelOfCar = "Tesla Model S P100D Ludicrous",
                    Brand = "Tesla",
                    Mileage = 1500,
                    Year = 2023,
                    CarCategoryId = 1,
                    Description = "The best electric car",
                    OwnerId = "d6b3ac1f-4fc8-d726-83d9-6d5800ce591e",
                    IsActive = true,
                    Price = 99998.00m,
                    ImageUrl = "https://g1-bg.cars.bg/2024-01-25_2/65b241748c69930ca00ce4a5o.jpg"
                },
                new Car()
                {
                    Id = 4,
                    ModelOfCar = "Lamborghini Aventador SCarbonRoadster",
                    Brand = "Lamborghini",
                    Mileage = 5000,
                    Year = 2018,
                    CarCategoryId = 2,
                    Description = "The best Lamborghini of your needs",
                    OwnerId = "d6b3ac1f-4fc8-d726-83d9-6d5800ce591e",
                    IsActive = true,
                    Price = 787000.00m,
                    ImageUrl = "https://g1-bg.cars.bg/2023-04-13_1/6437991c6e760656610960a2o.jpg"
                },
                new Car()
                {
                    Id = 5,
                    ModelOfCar = "Ferrari F8 Tributo",
                    Brand = "Ferrari",
                    Mileage = 200,
                    Year = 2021,
                    CarCategoryId = 2,
                    Description = "The best Ferrari of your needs",
                    OwnerId = "d6b3ac1f-4fc8-d726-83d9-6d5800ce591e",
                    IsActive = true,
                    Price = 99998.00m,
                    ImageUrl = "https://g1-bg.cars.bg/2023-03-20_2/641855494eccd5535d08a6e2o.jpg"
                },
                new Car()
                {
                    Id = 6,
                    ModelOfCar = "McLaren 600 LT Clubsport",
                    Brand = "McLaren",
                    Mileage = 700,
                    Year = 2019,
                    CarCategoryId = 2,
                    Description = "The best McLaren of your needs.",
                    OwnerId = "d6b3ac1f-4fc8-d726-83d9-6d5800ce591e",
                    IsActive = true,
                    Price = 489000.00m,
                    ImageUrl = "https://g1-bg.cars.bg/2023-04-13_1/64378e28d3b9b43ce0008b32o.jpg"
                },
                new Car()
                {
                    Id = 7,
                    ModelOfCar = "Aston Martin DB11 V8 Coupe",
                    Brand = "Aston Martin",
                    Mileage = 400,
                    Year = 2023,
                    CarCategoryId = 4,
                    Description = "The best Aston Martin for your needs",
                    OwnerId = "d6b3ac1f-4fc8-d726-83d9-6d5800ce591e",
                    IsActive = true,
                    Price = 461.00m,
                    ImageUrl = "https://g1-bg.cars.bg/2023-04-13_1/64379b4670903523a40449b6o.jpg"
                },

             };

            return cars;
        }
    }
}
