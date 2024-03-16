using AutoRepairService.Core.IServices;
using AutoRepairService.Core.Services;
using AutoRepairService.Infrastructure.Data;
using AutoRepairService.Infrastructure.Data.Common;
using AutoRepairService.Infrastructure.Data.EntityModels;
using Microsoft.EntityFrameworkCore;

namespace Skydiving.UnitTests
{
    public class OrderServiceTests
    {
        private IRepository repo;
        private IOrderService service;
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
        public async Task TestAllOrdersAsyncReturnsValidData()
        {
            service = new OrderService(repo);

            var user = new User() { Id = "newUserId", IsMechanic = false };

            var ordersList = new List<Order>()
            {
                new Order(){ Id = 101, ItemsDetails = "", TotalCost = 1, Client = user, ClientId = user.Id, IsCompleted = false, OrderAdress = "" , ReceivedOn = DateTime.Now, Status = "" },

                new Order(){ Id = 102, ItemsDetails = "", TotalCost = 1, Client = user, ClientId = user.Id, IsCompleted = true, OrderAdress = "" , ReceivedOn = DateTime.Now, CompletedOn = DateTime.Now, Status = "" },

                new Order(){ Id = 103, ItemsDetails = "", TotalCost = 1, Client = user, ClientId = user.Id, IsCompleted = false, OrderAdress = "" , ReceivedOn = DateTime.Now, Status = "" },

                new Order(){ Id = 104, ItemsDetails = "", TotalCost = 1, Client = user, ClientId = user.Id, IsCompleted = true, OrderAdress = "" , ReceivedOn = DateTime.Now, Status = "", CompletedOn = DateTime.Now}

            };

            await repo.AddRangeAsync(ordersList);
            await repo.SaveChangesAsync();

            var orders = await service.AllOrdersAsync();

            Assert.That(4, Is.EqualTo(orders.Count()));
            Assert.That(orders.ElementAt(0).OrderNumber == 101);
            Assert.That(orders.ElementAt(1).IsCompleted == true);
            Assert.That(orders.ElementAt(2).OrderNumber == 103);
            Assert.That(orders.ElementAt(3).IsCompleted == true);
        }

        [Test]
        public async Task TestDispatchAsync()
        {
            service = new OrderService(repo);

            var user = new User() { Id = "newUserId", IsMechanic = false };

            var ordersList = new List<Order>()
            {
                new Order(){ Id = 101, ItemsDetails = "", TotalCost = 1, Client = user, ClientId = user.Id, IsCompleted = false, OrderAdress = "" , ReceivedOn = DateTime.Now, Status = "" },


                new Order(){ Id = 103, ItemsDetails = "", TotalCost = 1, Client = user, ClientId = user.Id, IsCompleted = false, OrderAdress = "" , ReceivedOn = DateTime.Now, Status = "" },

            };
            await repo.AddRangeAsync(ordersList);
            await repo.SaveChangesAsync();

            await service.DispatchAsync(ordersList[0].Id);

            var orders = await service.AllOrdersAsync();

            Assert.That(orders.ElementAt(0).OrderNumber == 101);
            Assert.That(orders.ElementAt(0).IsCompleted == true);
            Assert.That(orders.ElementAt(0).Status == "Dispatched");
            Assert.That(orders.ElementAt(1).OrderNumber == 103);
            Assert.That(orders.ElementAt(1).IsCompleted == false);
            Assert.That(orders.ElementAt(1).Status == "");
        }
        [TearDown]
        public void TearDown()
        {
            context.Dispose();
        }
    }
}
