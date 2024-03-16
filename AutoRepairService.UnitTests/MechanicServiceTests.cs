using AutoRepairService.Core.IServices;
using AutoRepairService.Core.Services;
using AutoRepairService.Core.ViewModels.Mechanic;
using AutoRepairService.Infrastructure.Data;
using AutoRepairService.Infrastructure.Data.Common;
using AutoRepairService.Infrastructure.Data.EntityModels;
using Microsoft.EntityFrameworkCore;

namespace Skydiving.UnitTests
{
    public class MechanicServiceTests
    {
        private IRepository repo;
        private IMechanicService service;
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
        public async Task AddMechanicAsync()
        {
            service = new MechanicService(repo);

            var user = new User() { Id = "newUserId1", IsMechanic = false };

            await repo.AddAsync(user);
            await repo.SaveChangesAsync();

            var model = new BecomeMechanicViewModel()
            {
                FirstName = "FirstName",
                LastName = "LastName",
                PhoneNumber = "0899899889"
            };
            await service.AddMechanicAsync(user.Id, model);

            var userEntity = await repo.AllReadonly<User>().Where(x => x.Id == user.Id).FirstAsync();


            Assert.That(userEntity.FirstName, Is.EqualTo("FirstName"));
            Assert.That(userEntity.LastName, Is.EqualTo("LastName"));
            Assert.That(userEntity.PhoneNumber, Is.EqualTo("0899899889"));
            Assert.That(userEntity.IsMechanic, Is.EqualTo(true));
        }

        [Test]
        public async Task AddMechanicAsyncThrowsException()
        {
            service = new MechanicService(repo);

            var user = new User() { Id = "newUserId1", IsMechanic = false };

            await repo.AddAsync(user);
            await repo.SaveChangesAsync();

            var model = new BecomeMechanicViewModel()
            {
                FirstName = "FirstName",
                LastName = "LastName",
                PhoneNumber = "0899899889"
            };


            Assert.That(async () => await service.AddMechanicAsync("invalidUserId", model), Throws.Exception
             .With.Property("Message").EqualTo("User not found!"));
        }


        [Test]
        public async Task AllMechanicsAsync()
        {
            service = new MechanicService(repo);

            var newUsers = new List<User>()
            {
                new User() { Id = "newUserId1", IsMechanic = true, FirstName = "", LastName = "", PhoneNumber = "" },
                new User() { Id = "newUserId2", IsMechanic = true, FirstName = "", LastName = "", PhoneNumber = "" },
                new User() { Id = "newUserId3", IsMechanic = true, FirstName = "", LastName = "", PhoneNumber = "" },
                new User() { Id = "newUserId4", IsMechanic = false }
            };

            await repo.AddRangeAsync(newUsers);
            await repo.SaveChangesAsync();

            var result = await service.AllMechanicsAsync();

            Assert.That(3, Is.EqualTo(result.Count()));
            Assert.That(result.ElementAt(0).Id == "newUserId1");
            Assert.That(result.ElementAt(1).Id == "newUserId2");
            Assert.That(result.ElementAt(2).Id == "newUserId3");
        }

        [Test]
        public async Task RateMechanicAsync_MechanicRatingAsync()
        {
            service = new MechanicService(repo);

            var newUsers = new List<User>()
            {
                new User() { Id = "newUserId1", IsMechanic = true, FirstName = "", LastName = "", PhoneNumber = "" },
                new User() { Id = "newUserId2", IsMechanic = false },
                new User() { Id = "newUserId3", IsMechanic = false }
            };
            await repo.AddRangeAsync(newUsers);

            var repairs = new List<Repair>()
            {
                new Repair(){ Id = 1, IsActive = true, IsTaken = true, MechanicId = "newUserId1", OwnerId ="newUserId2", Description ="", Brand = "", CarModel = ""},
                 new Repair(){ Id = 2, IsActive = true, IsTaken = true, MechanicId = "newUserId1", OwnerId ="newUserId3", Description = "", Brand = "", CarModel = ""},
            };
            await repo.AddRangeAsync(repairs);
            await repo.SaveChangesAsync();

            var model1 = new MechanicRatingModel()
            {
                MechanicId = "newUserId1",
                Comment = "comment1",
                RepairId = 1,
                Points = 5,
                UserId = "newUserId2"
            };

            await service.RateMechanicAsync("newUserId2", "newUserId1", 1, model1);

            var firstRatingIsAdded = await repo.AllReadonly<Rating>().Where(x => x.RepairId == 1 && x.UserId == "newUserId2" && x.MechanicId == "newUserId1" && x.Comment == "comment1" && x.Points == 5).AnyAsync();

            Assert.True(firstRatingIsAdded);


            var model2 = new MechanicRatingModel()
            {
                MechanicId = "newUserId1",
                Comment = "comment2",
                RepairId = 2,
                Points = 4,
                UserId = "newUserId3"
            };

            await service.RateMechanicAsync("newUserId3", "newUserId1", 2, model2);

            var secondRatingIsAdded = await repo.AllReadonly<Rating>().Where(x => x.RepairId == 2 && x.UserId == "newUserId3" && x.MechanicId == "newUserId1" && x.Comment == "comment2" && x.Points == 4).AnyAsync();

            Assert.True(secondRatingIsAdded);


            var ratingData = await service.MechanicRatingAsync("newUserId1");
            var data = "4.50 / 5 (2 completed repairs)";

            Assert.That(ratingData, Is.EqualTo(data));
        }


        [Test]
        public async Task RateMechanicAsyncThrowsException()
        {
            service = new MechanicService(repo);

            var newUsers = new List<User>()
            {
                new User() { Id = "newUserId1", IsMechanic = true, FirstName = "", LastName = "", PhoneNumber = "" },
                new User() { Id = "newUserId2", IsMechanic = false },
            };
            await repo.AddRangeAsync(newUsers);

            var repairs = new List<Repair>()
            {
                new Repair(){ Id = 1, IsActive = true, IsTaken = true, MechanicId = "newUserId1", OwnerId ="newUserId2", Description ="", Brand = "", CarModel = ""}
            };
            await repo.AddRangeAsync(repairs);
            await repo.SaveChangesAsync();

            var model1 = new MechanicRatingModel()
            {
                MechanicId = "newUserId1",
                Comment = "comment1",
                RepairId = 1,
                Points = 5,
                UserId = "newUserId2"
            };


            Assert.That(async () => await service.RateMechanicAsync("newUserId1", "newUserId1", 1, model1),
                Throws.Exception.With.Property("Message").EqualTo("You can't rate yourself!"));


            Assert.That(async () => await service.RateMechanicAsync("invalid", "newUserId1", 1, model1),
                Throws.Exception.With.Property("Message").EqualTo("Invalid user Id"));


            Assert.That(async () => await service.RateMechanicAsync("newUserId2", "invalid", 1, model1),
                Throws.Exception.With.Property("Message").EqualTo("Invalid user Id"));


            Assert.That(async () => await service.RateMechanicAsync("newUserId2", "newUserId1", 2, model1),
                Throws.Exception.With.Property("Message").EqualTo("Repair don't exist!"));


            await service.RateMechanicAsync("newUserId2", "newUserId1", 1, model1);

            Assert.That(async () => await service.RateMechanicAsync("newUserId2", "newUserId1", 1, model1),
                Throws.Exception.With.Property("Message").EqualTo("Repair is already rated!"));

            //await service.RateMechanicAsync("newUserId2", "newUserId1", 1, model1);

            //var firstRatingIsAdded = await repo.AllReadonly<Rating>().Where(x => x.RepairId == 1 && x.UserId == "newUserId2" && x.MechanicId == "newUserId1" && x.Comment == "comment1" && x.Points == 5).AnyAsync();

            //Assert.True(firstRatingIsAdded);


            //var model2 = new MechanicRatingModel()
            //{
            //    MechanicId = "newUserId1",
            //    Comment = "comment2",
            //    RepairId = 2,
            //    Points = 4,
            //    UserId = "newUserId3"
            //};

            //await service.RateMechanicAsync("newUserId3", "newUserId1", 2, model2);

            //var secondRatingIsAdded = await repo.AllReadonly<Rating>().Where(x => x.RepairId == 2 && x.UserId == "newUserId3" && x.MechanicId == "newUserId1" && x.Comment == "comment2" && x.Points == 4).AnyAsync();

            //Assert.True(secondRatingIsAdded);


            //var ratingData = await service.MechanicRatingAsync("newUserId1");
            //var data = "4.50 / 5 (2 completed repairs)";

            //Assert.That(ratingData, Is.EqualTo(data));
        }



        [TearDown]
        public void TearDown()
        {
            context.Dispose();
        }
    }
}
