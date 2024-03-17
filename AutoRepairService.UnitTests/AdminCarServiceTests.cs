
using AutoRepairService.Core.IServices;
using AutoRepairService.Core.Services;
using AutoRepairService.Core.ViewModels.Car;
using AutoRepairService.Infrastructure.Data;
using AutoRepairService.Infrastructure.Data.Common;
using AutoRepairService.Infrastructure.Data.EntityModels;
using Microsoft.EntityFrameworkCore;

namespace Skydiving.UnitTests
{
    public class AdminCarServiceTests
    {
        private IRepository repo;
        private IAdminCarService service;
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

        [Test]
        public async Task AddCarAsync()
        {
            service = new AdminCarService(repo);

            var user = new User() { Id = "userId", IsMechanic = false };
            await repo.AddAsync(user);
            await repo.SaveChangesAsync();

            var model = new CarModel()
            {
                Brand = "Brand",
                ModelOfCar = "ModelOfCar",
                Description = "Description",
                Price = 1,
                Mileage = 1,
                Year = 1,
                CategoryId = 1,
                ImageUrl = "",
            };

            var carBefore = await repo.AllReadonly<Car>().Where(x => x.OwnerId == user.Id).AnyAsync();
            Assert.IsFalse(carBefore);

            await service.AddCarAsync(model, user.Id);

            var carAfter = await repo.AllReadonly<Car>().Where(x => x.OwnerId == user.Id).AnyAsync();
            Assert.IsTrue(carAfter);
        }

        [Test]
        public void AddCarAsyncThrowsException()
        {
            service = new AdminCarService(repo);

            var model = new CarModel()
            {
                Brand = "Brand",
                ModelOfCar = "ModelOfCar",
                Description = "Description",
                Mileage = 1,
                Year = 1,
                Price = 1,
                CategoryId = 1,
                ImageUrl = "",
            };


            Assert.That(async () => await service.AddCarAsync(model, "invalidId"), Throws.Exception
                .With.Property("Message").EqualTo("User entity error"));
        }

        [Test]
        public async Task AllCategories()
        {
            service = new AdminCarService(repo);

            await repo.AddRangeAsync(new List<CarCategory>()
            {
                new CarCategory(){ Id = 1, Name = "1"},
                new CarCategory(){ Id = 2, Name = "2"},
                new CarCategory(){ Id = 3, Name = "3"}
            });
            await repo.SaveChangesAsync();

            var categories = await service.AllCategories();

            Assert.That(3, Is.EqualTo(categories.Count()));
            Assert.That(categories.ElementAt(0).Name == "1");
            Assert.That(categories.ElementAt(1).Name == "2");
            Assert.That(categories.ElementAt(2).Name == "3");
        }

        [Test]
        public async Task CategoryExists()
        {
            service = new AdminCarService(repo);

            await repo.AddRangeAsync(new List<CarCategory>()
            {
                new CarCategory(){ Id = 1, Name = "1"},
                new CarCategory(){ Id = 2, Name = "2"}
            });
            await repo.SaveChangesAsync();

            var category1 = await service.CategoryExists(1);
            var category2 = await service.CategoryExists(2);
            var category3 = await service.CategoryExists(3);
            var category4 = await service.CategoryExists(4);

            Assert.IsTrue(category1);
            Assert.IsTrue(category2);
            Assert.IsFalse(category3);
            Assert.IsFalse(category4);
        }

        //[Test]
        //public async Task CarExistAsync()
        //{
        //    service = new AdminCarService(repo);

        //    await repo.AddRangeAsync(new List<Car>()
        //    {
        //        new Car(){ Id = 1,Brand ="",Quantity = 1,CarCategoryId =1, Description = "", OwnerId ="", IsActive = true, ImageUrl = "", Price = 1, ModelOfCar = ""},
        //        new Car(){ Id = 2,Brand ="",Quantity = 1,CarCategoryId =1, Description = "", OwnerId ="", IsActive = true, ImageUrl = "", Price = 1, ModelOfCar =""}
        //    });
        //    await repo.SaveChangesAsync();

        //    var car1 = await service.CarExistAsync(1);
        //    var car2 = await service.CarExistAsync(2);
        //    var car3 = await service.CarExistAsync(3);
        //    var car4 = await service.CarExistAsync(4);

        //    Assert.IsTrue(car1);
        //    Assert.IsTrue(car2);
        //    Assert.IsFalse(car3);
        //    Assert.IsFalse(car4);
        //}

        [Test]
        public async Task GetEdit()
        {
            service = new AdminCarService(repo);

            var user = new User() { Id = "userId" };
            await repo.AddAsync(user);

            await repo.AddAsync(new Car()
            {
                Id = 1,
                Brand = "Brand",
                ModelOfCar = "ModelOfCar",
                CarCategoryId = 1,
                Description = "Description",
                OwnerId = user.Id,
                IsActive = true,
                ImageUrl = "",
                Price = 1,
                Mileage = 1,
                Year = 1,
                Owner = user
            });

            await repo.SaveChangesAsync();

            var model = await service.GetEditAsync(1, "userId");

            Assert.That(model.Description, Is.EqualTo("Description"));
            Assert.That(model.Price, Is.EqualTo(1));
            Assert.That(model.Brand, Is.EqualTo("Brand"));
            Assert.That(model.CategoryId, Is.EqualTo(1));
            //Assert.That(model.Quantity, Is.EqualTo(1));
            Assert.That(model.Brand, Is.EqualTo("Brand"));
            Assert.That(model.ModelOfCar, Is.EqualTo("ModelOfCar"));
        }

        [Test]
        public async Task GetEditThrowsException()
        {
            service = new AdminCarService(repo);

            var user = new User() { Id = "userId" };
            await repo.AddAsync(user);

            await repo.AddAsync(new Car()
            {
                Id = 1,
                Brand = "Brand",
                ModelOfCar = "ModelOfCar",
                CarCategoryId = 1,
                Description = "Description",
                OwnerId = user.Id,
                IsActive = false,
                ImageUrl = "",
                Price = 1,
                Mileage = 1,
                Year = 1,
                Owner = user
            });

            await repo.SaveChangesAsync();

            Assert.That(async () => await service.GetEditAsync(2, "userId"), Throws.Exception
                .With.Property("Message").EqualTo("Car don't exist!"));

            Assert.That(async () => await service.GetEditAsync(1, "invalidUserId"), Throws.Exception
                .With.Property("Message").EqualTo("User is not owner"));

            Assert.That(async () => await service.GetEditAsync(1, "userId"), Throws.Exception
                .With.Property("Message").EqualTo("This car is not active"));


        }

        [Test]
        public async Task PostEdit()
        {
            service = new AdminCarService(repo);

            await repo.AddAsync(new Car() { Id = 1, Brand = "", ModelOfCar = "", Mileage = 1, Year = 1, CarCategoryId = 1, Description = "", OwnerId = "", IsActive = true, ImageUrl = "", Price = 1});

            await repo.SaveChangesAsync();

            await service.PostEditAsync(1, new CarModel()
            {
                CategoryId = 2,
                Brand = "Brand",
                ModelOfCar = "ModelOfCar",
                Description = "Description",
                ImageUrl = "",
                Price = 200,
                Mileage = 1,
                Year = 1,
            });

            var car = await repo.AllReadonly<Car>().Where(x => x.Id == 1).FirstOrDefaultAsync();

            Assert.That(car.Description, Is.EqualTo("Description"));
            Assert.That(car.Price, Is.EqualTo(200));
            Assert.That(car.Brand, Is.EqualTo("Brand"));
            Assert.That(car.CarCategoryId, Is.EqualTo(2));
            //Assert.That(car.Quantity, Is.EqualTo(12));
            Assert.That(car.Brand, Is.EqualTo("Brand"));
            Assert.That(car.ModelOfCar, Is.EqualTo("ModelOfCar"));
        }

        [Test]
        public async Task PostEditThrowsException()
        {
            service = new AdminCarService(repo);

            await repo.AddAsync(new Car() { Id = 1, Brand = "", ModelOfCar = "", Mileage = 1, Year = 1, CarCategoryId = 1, Description = "", OwnerId = "", IsActive = true, ImageUrl = "", Price = 1 });

            await repo.SaveChangesAsync();
            var model = new CarModel()
            {
                CategoryId = 2,
                Brand = "Brand",
                ModelOfCar = "ModelOfCar",
                Description = "Description",
                ImageUrl = "",
                Mileage = 1,
                Year = 1,
            };


            Assert.That(async () => await service.PostEditAsync(2, model), Throws.Exception
                .With.Property("Message").EqualTo("Car don't exist!"));
        }

        [Test]
        public async Task RemoveCarAsync()
        {
            service = new AdminCarService(repo);

            await repo.AddAsync(new Car() { Id = 1, Brand = "", ModelOfCar = "", Mileage = 1, Year = 1, CarCategoryId = 1, Description = "", OwnerId = "ownerId", IsActive = true, ImageUrl = "", Price = 1});

            await repo.SaveChangesAsync();

            var carBefore = await repo.AllReadonly<Car>().Where(x => x.Id == 1 && x.IsActive == true).AnyAsync();
            Assert.IsTrue(carBefore);

            await service.RemoveCarAsync(1, "ownerId");

            var carAfter = await repo.AllReadonly<Car>().Where(x => x.Id == 1 && x.IsActive == false).AnyAsync();
            Assert.IsTrue(carAfter);

        }

        [Test]
        public async Task RemoveCarAsyncThrowsException()
        {
            service = new AdminCarService(repo);

            await repo.AddAsync(new Car() { Id = 1, Brand = "", ModelOfCar = "", Mileage = 1, Year = 1, CarCategoryId = 1, Description = "", OwnerId = "ownerId", IsActive = true, ImageUrl = "", Price = 1 });

            await repo.SaveChangesAsync();


            Assert.That(async () => await service.RemoveCarAsync(2, "ownerId"), Throws.Exception
                .With.Property("Message").EqualTo("Invalid car Id"));

            Assert.That(async () => await service.RemoveCarAsync(1, "invalidId"), Throws.Exception
               .With.Property("Message").EqualTo("Invalid user Id"));
        }



        [TearDown]
        public void TearDown()
        {
            context.Dispose();
        }
    }
}
