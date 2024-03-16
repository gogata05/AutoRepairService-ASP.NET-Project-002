

//Add thrown exceptions

using AutoRepairService.Core.IServices;
using AutoRepairService.Core.Services;
using AutoRepairService.Infrastructure.Data;
using AutoRepairService.Infrastructure.Data.Common;
using AutoRepairService.Infrastructure.Data.EntityModels;
using Microsoft.EntityFrameworkCore;

namespace Skydiving.UnitTests
{
    public class CarServiceTests
    {
        private IRepository repo;
        private ICarService service;
        private ApplicationDbContext context;


        [SetUp]
        public void Setup()
        {
            var contextOptions = new DbContextOptionsBuilder<ApplicationDbContext>()
               .UseInMemoryDatabase("AutoRepairService_DB")
               .Options;

            context = new ApplicationDbContext(contextOptions);
            repo = new Repository(context);

            context.Database.EnsureDeleted();
            context.Database.EnsureCreated();
        }

        //[Test]
        //public async Task TestAllCarsAsyncReturnsValidData()
        //{
        //    Assert.Pass();

        //    //Implement logic
        //}

        [Test]
        public async Task TestAllCategoriesNamesReturnsValidData()
        {
            service = new CarService(repo);

            await repo.AddRangeAsync(new List<CarCategory>()
            {
                new CarCategory(){ Id = 101 , Name = "First" },
                new CarCategory(){ Id = 102 , Name = "Second" },
                new CarCategory(){ Id = 103 , Name = "Second" }
            });

            await repo.SaveChangesAsync();

            var categoryNames = await service.AllCategoriesNames();

            Assert.That(2, Is.EqualTo(categoryNames.Count()));
            Assert.AreEqual(categoryNames, new List<string>() { "First", "Second" });
        }

        [Test]
        public async Task TestLastThreeCarsReturnsValidData()
        {
            service = new CarService(repo);

            await repo.AddRangeAsync(new List<Car>()
            {
                new Car(){Id = 101,ImageUrl ="", Price = 1, OwnerId = "",Description = "", CarCategoryId = 1, Brand = "", ModelOfCar = ""},
                new Car(){Id = 102,ImageUrl ="", Price = 1, OwnerId = "",Description = "", CarCategoryId = 1, Brand = "", ModelOfCar = ""},
                new Car(){Id = 105,ImageUrl ="", Price = 1, OwnerId = "",Description = "", CarCategoryId = 1, Brand = "", ModelOfCar = ""},
                new Car(){Id = 107,ImageUrl ="", Price = 1, OwnerId = "",Description = "", CarCategoryId = 1, Brand = "", ModelOfCar = ""}
            });

            await repo.SaveChangesAsync();

            var cars = await service.GetLastThreeCars();

            Assert.That(3, Is.EqualTo(cars.Count()));
            Assert.That(cars.Any(h => h.Id == 101), Is.False);
        }

        [Test]
        public async Task TestGetAllCarsAsyncReturnsValidData()
        {
            var user = new User() { Id = "newUserId", IsMechanic = false };
            var category = new CarCategory() { Id = 1001, Name = "Category" };

            service = new CarService(repo);

            var carList = new List<Car>()
            {
                new Car(){Id = 107,ImageUrl ="", Price = 1, OwnerId = "",Owner = user, Category = category, Description = "", CarCategoryId = 1, Brand = "", ModelOfCar = ""},
                new Car(){Id = 106,ImageUrl ="", Price = 1, OwnerId = "",Owner = user, Category = category, Description = "", CarCategoryId = 1, Brand = "", ModelOfCar = ""},
                new Car(){Id = 105,ImageUrl ="", Price = 1, OwnerId = "",Owner = user, Category = category, Description = "", CarCategoryId = 1, Brand = "", ModelOfCar = ""},
                new Car(){Id = 104,ImageUrl ="", Price = 1, OwnerId = "",Owner = user, Category = category, Description = "", CarCategoryId = 1, Brand = "", ModelOfCar = ""},
                new Car(){Id = 103,ImageUrl ="", Price = 1, OwnerId = "",Owner = user, Category = category, Description = "", CarCategoryId = 1, Brand = "", ModelOfCar = ""},
                new Car(){Id = 102,ImageUrl ="", Price = 1, OwnerId = "",Owner = user, Category = category, Description = "", CarCategoryId = 1, Brand = "", ModelOfCar = ""},
                new Car(){Id = 101,ImageUrl ="", Price = 1, OwnerId = "",Owner = user, Category = category, Description = "", CarCategoryId = 1, Brand = "", ModelOfCar = ""}
            };

            await repo.AddRangeAsync(carList);

            await repo.SaveChangesAsync();

            var cars = await service.GetAllCarsAsync();

            Assert.That(7, Is.EqualTo(cars.Count()));


        }

        [TearDown]
        public void TearDown()
        {
            context.Dispose();
        }
    }
}
