using AutoRepairService.Core.IServices;
using AutoRepairService.Core.Services;
using AutoRepairService.Core.ViewModels;
using AutoRepairService.Infrastructure.Data;
using AutoRepairService.Infrastructure.Data.Common;
using AutoRepairService.Infrastructure.Data.EntityModels;
using Microsoft.EntityFrameworkCore;

namespace AutoRepairService.UnitTests
{
    public class RepairServiceTests
    {
        private IRepository repo;
        private IRepairService service;
        private IMechanicService mechanicService;
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
        public async Task AddRepairAsync()
        {
            mechanicService = new MechanicService(repo);
            service = new RepairService(repo, mechanicService);

            var user = new User() { Id = "userId1", IsMechanic = false };
            await repo.AddAsync(user);
            await repo.SaveChangesAsync();

            var model = new RepairModel()
            {
                Brand = "Test",
                CarModel = "",
                Description = "",
                CategoryId = 1,
                Owner = user,
                OwnerName = ""
            };

            var repairBefore = await repo.AllReadonly<Repair>()
                .Where(x => x.OwnerId == user.Id && x.Brand == "Test")
                .AnyAsync();

            Assert.IsFalse(repairBefore);

            await service.AddRepairAsync(user.Id, model);

            var repairAfter = await repo.AllReadonly<Repair>()
                .Where(x => x.OwnerId == user.Id && x.Brand == "Test")
                .AnyAsync();

            Assert.IsTrue(repairAfter);
        }

        [Test]
        public async Task AddRepairAsyncThrowsException()
        {
            mechanicService = new MechanicService(repo);
            service = new RepairService(repo, mechanicService);

            var user = new User() { Id = "userId", IsMechanic = false };
            await repo.AddAsync(user);
            await repo.SaveChangesAsync();

            var model = new RepairModel()
            {
                Brand = "Test",
                CarModel = "",
                Description = "",
                CategoryId = 1,
                Owner = user,
                OwnerName = ""
            };

            Assert.That(async () => await service.AddRepairAsync("invalidId", model),
                Throws.Exception.With.Property("Message").EqualTo("User not found"));
        }

        [Test]
        public async Task GetEditAsync()
        {
            mechanicService = new MechanicService(repo);
            service = new RepairService(repo, mechanicService);

            var user = new User() { Id = "userId", IsMechanic = false };
            await repo.AddAsync(user);

            var repair = new Repair()
            {
                Brand = "TestBrand",
                CarModel = "TestCarModel",
                Description = "TestDescription",
                RepairCategoryId = 1,
                Id = 1,
                IsActive = true,
                IsApproved = true,
                IsTaken = false,
                RepairStatusId = 1,
                Owner = user,
                OwnerId = user.Id,
                StartDate = DateTime.Now,
                Status = ""
            };
            await repo.AddAsync(repair);
            await repo.SaveChangesAsync();

            var repairAdded = await repo.AllReadonly<Repair>()
                .Where(x => x.OwnerId == user.Id && x.Brand == "TestBrand" && x.CarModel == "TestCarModel" && x.Description == "TestDescription")
                .AnyAsync();

            Assert.IsTrue(repairAdded);

            var model = await service.GetEditAsync(1, user.Id);

            Assert.That(model.Description, Is.EqualTo("TestDescription"));
            Assert.That(model.Brand, Is.EqualTo("TestBrand"));
            Assert.That(model.CarModel, Is.EqualTo("TestCarModel"));
        }

        [Test]
        public async Task GetEditAsyncThrowsExcepion()
        {
            mechanicService = new MechanicService(repo);
            service = new RepairService(repo, mechanicService);

            var user = new User() { Id = "userId", IsMechanic = false };
            await repo.AddAsync(user);

            var user2 = new User() { Id = "userId2", IsMechanic = false };
            await repo.AddAsync(user2);

            var repair = new Repair()
            {
                Brand = "TestBrand",
                CarModel = "TestCarModel",
                Description = "TestDescription",
                RepairCategoryId = 1,
                Id = 1,
                IsActive = true,
                IsApproved = true,
                IsTaken = false,
                RepairStatusId = 1,
                Owner = user,
                OwnerId = user.Id,
                StartDate = DateTime.Now,
                Status = ""
            };
            await repo.AddAsync(repair);
            var repair2 = new Repair()
            {
                Brand = "TestBrand",
                CarModel = "TestCarModel",
                Description = "TestDescription",
                RepairCategoryId = 1,
                Id = 2,
                IsActive = true,
                IsApproved = true,
                IsTaken = true,
                RepairStatusId = 1,
                Owner = user,
                OwnerId = user.Id,
                StartDate = DateTime.Now,
                Status = ""
            };
            await repo.AddAsync(repair2);
            var repair3 = new Repair()
            {
                Brand = "TestBrand",
                CarModel = "TestCarModel",
                Description = "TestDescription",
                RepairCategoryId = 1,
                Id = 3,
                IsActive = true,
                IsApproved = false,
                IsTaken = false,
                RepairStatusId = 1,
                Owner = user,
                OwnerId = user.Id,
                StartDate = DateTime.Now,
                Status = ""
            };
            await repo.AddAsync(repair3);
            await repo.SaveChangesAsync();

            var repairAdded = await repo.AllReadonly<Repair>()
                .Where(x => x.OwnerId == user.Id && x.Brand == "TestBrand" && x.CarModel == "TestCarModel" && x.Description == "TestDescription")
                .AnyAsync();

            Assert.IsTrue(repairAdded);


            Assert.That(async () => await service.GetEditAsync(4, user.Id),
                Throws.Exception.With.Property("Message")
                .EqualTo("Repair not found"));

            Assert.That(async () => await service.GetEditAsync(1, "invalidId"),
               Throws.Exception.With.Property("Message")
               .EqualTo("Owner not found"));

            Assert.That(async () => await service.GetEditAsync(1, user2.Id),
                Throws.Exception.With.Property("Message")
                .EqualTo("User is not owner"));

            Assert.That(async () => await service.GetEditAsync(2, user.Id),
                Throws.Exception.With.Property("Message")
                .EqualTo("Can't edit ongoing repair"));


            Assert.That(async () => await service.GetEditAsync(3, user.Id),
                Throws.Exception.With.Property("Message")
                .EqualTo("This repair is not approved"));
        }


        [Test]
        public async Task PostEditAsync()
        {
            mechanicService = new MechanicService(repo);
            service = new RepairService(repo, mechanicService);

            var user = new User() { Id = "userId", IsMechanic = false };
            await repo.AddAsync(user);

            var repair = new Repair()
            {
                Brand = "TestBrand",
                CarModel = "TestCarModel",
                Description = "TestDescription",
                RepairCategoryId = 1,
                Id = 1,
                IsActive = true,
                IsApproved = true,
                IsTaken = false,
                RepairStatusId = 1,
                Owner = user,
                OwnerId = user.Id,
                StartDate = DateTime.Now,
                Status = ""
            };
            await repo.AddAsync(repair);
            await repo.SaveChangesAsync();

            var repairAdded = await repo.AllReadonly<Repair>()
                .Where(x => x.OwnerId == user.Id && x.Brand == "TestBrand" && x.CarModel == "TestCarModel" && x.Description == "TestDescription")
                .AnyAsync();

            Assert.IsTrue(repairAdded);

            var model = new RepairModel()
            {
                Brand = "EditBrand",
                CarModel ="EditCarModel",
                Description = "EditDescription",
                CategoryId = 2,
                Owner = user
            };

            await service.PostEditAsync(1, model);

            var editedRepair = await repo.AllReadonly<Repair>()
                .Where(x => x.OwnerId == user.Id)
                .FirstAsync();

            Assert.That(editedRepair.Brand, Is.EqualTo("EditBrand"));
            Assert.That(editedRepair.CarModel, Is.EqualTo("EditCarModel"));
            Assert.That(editedRepair.Description, Is.EqualTo("EditDescription"));
            Assert.That(editedRepair.RepairCategoryId, Is.EqualTo(2));
        }

        [Test]
        public async Task PostEditAsyncThrowsException()
        {
            mechanicService = new MechanicService(repo);
            service = new RepairService(repo, mechanicService);

            var user = new User() { Id = "userId", IsMechanic = false };
            await repo.AddAsync(user);

            var repair = new Repair()
            {
                Brand = "TestBrand",
                CarModel = "TestCarModel",
                Description = "TestDescription",
                RepairCategoryId = 1,
                Id = 1,
                IsActive = true,
                IsApproved = true,
                IsTaken = false,
                RepairStatusId = 1,
                Owner = user,
                OwnerId = user.Id,
                StartDate = DateTime.Now,
                Status = ""
            };
            await repo.AddAsync(repair);
            await repo.SaveChangesAsync();

            var repairAdded = await repo.AllReadonly<Repair>()
                .Where(x => x.OwnerId == user.Id && x.Brand == "TestBrand" && x.CarModel == "TestCarModel" && x.Description == "TestDescription")
                .AnyAsync();

            Assert.IsTrue(repairAdded);

            var model = new RepairModel()
            {
                Brand = "EditBrand",
                CarModel = "EditCarModel",
                Description = "EditDescription",
                CategoryId = 2,
                Owner = user
            };

            Assert.That(async () => await service.PostEditAsync(2, model),
               Throws.Exception.With.Property("Message")
               .EqualTo("Repair not found"));
            ;
        }

        [Test]
        public async Task GetAllRepairsAsync()
        {
            mechanicService = new MechanicService(repo);
            service = new RepairService(repo, mechanicService);

            var user = new User() { Id = "userId", IsMechanic = false };
            await repo.AddAsync(user);

            var category = new RepairCategory() { Id = 1, Name = "category" };

            await repo.AddAsync(category);

            await repo.AddRangeAsync(new List<Repair>()
            {
                new Repair(){Id = 1, Category = category, RepairCategoryId =1,  Description ="active1", Brand = "", CarModel = "", IsActive = true, IsApproved = true, IsTaken = false, Status = "Active", Owner = user, OwnerId = user.Id},

                new Repair(){Id = 2, Category = category, RepairCategoryId =1, Description ="taken", Brand = "", CarModel = "", IsActive = true, IsApproved = true, IsTaken = true, Status = "Taken", Owner = user, OwnerId = user.Id},

                new Repair(){Id = 3, Category = category, RepairCategoryId =1, Description ="active2", Brand = "", CarModel = "", IsActive = true, IsApproved = true, IsTaken = false, Status = "Active", Owner = user, OwnerId = user.Id},

                new Repair(){Id = 4, Category = category, RepairCategoryId =1, Description ="pending" ,Brand = "", CarModel = "", IsActive = true, IsApproved = false, IsTaken = false, Status = "Pending", Owner = user, OwnerId = user.Id},

                new Repair(){Id = 5, Category = category, RepairCategoryId =1, Description ="removed", Brand = "", CarModel = "", IsActive = false, IsApproved = true, IsTaken = false, Status = "Removed", Owner = user, OwnerId = user.Id}
            });
            await repo.SaveChangesAsync();


            var repairs = await service.GetAllRepairsAsync();

            Assert.That(2, Is.EqualTo(repairs.Count()));

            Assert.That(repairs.ElementAt(0).Description, Is.EqualTo("active1"));
            Assert.That(repairs.ElementAt(1).Description, Is.EqualTo("active2"));
        }

        [Test]
        public async Task RepairDetailsAsync()
        {
            mechanicService = new MechanicService(repo);
            service = new RepairService(repo, mechanicService);

            var user = new User() { Id = "userId", IsMechanic = false };
            await repo.AddAsync(user);

            var category = new RepairCategory() { Id = 1, Name = "category" };

            await repo.AddAsync(category);

            await repo.AddRangeAsync(new List<Repair>()
            {
                new Repair(){Id = 1, Category = category, RepairCategoryId =1,  Description ="active1", Brand = "", CarModel = "", IsActive = true, IsApproved = true, IsTaken = false, Status = "Active", Owner = user, OwnerId = user.Id},

                new Repair(){Id = 2, Category = category, RepairCategoryId =1, Description ="taken", Brand = "", CarModel = "", IsActive = true, IsApproved = true, IsTaken = true, Status = "Taken", Owner = user, OwnerId = user.Id},

                new Repair(){Id = 3, Category = category, RepairCategoryId =1, Description ="active2", Brand = "", CarModel = "", IsActive = true, IsApproved = true, IsTaken = false, Status = "Active", Owner = user, OwnerId = user.Id},

                new Repair(){Id = 4, Category = category, RepairCategoryId =1, Description ="pending" ,Brand = "", CarModel = "", IsActive = true, IsApproved = false, IsTaken = false, Status = "Pending", Owner = user, OwnerId = user.Id},

                new Repair(){Id = 5, Category = category, RepairCategoryId =1, Description ="removed", Brand = "", CarModel = "", IsActive = false, IsApproved = true, IsTaken = false, Status = "Removed", Owner = user, OwnerId = user.Id}
            });
            await repo.SaveChangesAsync();

            var repairsDetails = await service.RepairDetailsAsync(3);

            Assert.That(repairsDetails.Category, Is.EqualTo("category"));
            Assert.That(repairsDetails.Description, Is.EqualTo("active2"));
        }

        [Test]
        public async Task RepairDetailsAsyncThrowsException()
        {
            mechanicService = new MechanicService(repo);
            service = new RepairService(repo, mechanicService);

            var user = new User() { Id = "userId", IsMechanic = false };
            await repo.AddAsync(user);

            var category = new RepairCategory() { Id = 1, Name = "category" };

            await repo.AddAsync(category);

            await repo.AddRangeAsync(new List<Repair>()
            { 
                new Repair(){Id = 1, Category = category, RepairCategoryId =1,  Description ="active1", Brand = "", CarModel = "", IsActive = true, IsApproved = true, IsTaken = false, Status = "Active", Owner = user, OwnerId = user.Id},

                new Repair(){Id = 2, Category = category, RepairCategoryId =1, Description ="taken", Brand = "", CarModel = "", IsActive = true, IsApproved = true, IsTaken = true, Status = "Taken", Owner = user, OwnerId = user.Id},

                new Repair(){Id = 3, Category = category, RepairCategoryId =1, Description ="active2", Brand = "", CarModel = "", IsActive = true, IsApproved = true, IsTaken = false, Status = "Active", Owner = user, OwnerId = user.Id},

                new Repair(){Id = 4, Category = category, RepairCategoryId =1, Description ="pending" ,Brand = "", CarModel = "", IsActive = true, IsApproved = false, IsTaken = false, Status = "Pending", Owner = user, OwnerId = user.Id},

                new Repair(){Id = 5, Category = category, RepairCategoryId =1, Description ="removed", Brand = "", CarModel = "", IsActive = false, IsApproved = true, IsTaken = false, Status = "Removed", Owner = user, OwnerId = user.Id}
            });
            await repo.SaveChangesAsync();

            Assert.That(async () => await service.RepairDetailsAsync(6),
               Throws.Exception.With.Property("Message")
               .EqualTo("Repair not found"));
        }
        [Test]
        public async Task RepairExistAsync()
        {
            mechanicService = new MechanicService(repo);
            service = new RepairService(repo, mechanicService);

            var user = new User() { Id = "userId", IsMechanic = false };
            await repo.AddAsync(user);

            var category = new RepairCategory() { Id = 1, Name = "category" };

            await repo.AddAsync(category);

            await repo.AddRangeAsync(new List<Repair>()
            {
                new Repair(){Id = 1, Category = category, RepairCategoryId =1,  Description ="active1", Brand = "", CarModel = "", IsActive = true, IsApproved = true, IsTaken = false, Status = "Active", Owner = user, OwnerId = user.Id},

                new Repair(){Id = 2, Category = category, RepairCategoryId =1, Description ="taken", Brand = "", CarModel = "", IsActive = true, IsApproved = true, IsTaken = true, Status = "Taken", Owner = user, OwnerId = user.Id},

                new Repair(){Id = 3, Category = category, RepairCategoryId =1, Description ="active2", Brand = "", CarModel = "", IsActive = true, IsApproved = true, IsTaken = false, Status = "Active", Owner = user, OwnerId = user.Id},
            });
            await repo.SaveChangesAsync();

            var firstRepairExist = await service.RepairExistAsync(1);
            var secondRepairExist = await service.RepairExistAsync(2);
            var thirdRepairExist = await service.RepairExistAsync(3);
            var nonExistingRepair = await service.RepairExistAsync(4);

            Assert.IsTrue(firstRepairExist);
            Assert.IsTrue(secondRepairExist);
            Assert.IsTrue(thirdRepairExist);
            Assert.IsFalse(nonExistingRepair);
        }

        [Test]
        public async Task RepairOffersAsync()
        {
            mechanicService = new MechanicService(repo);
            service = new RepairService(repo, mechanicService);

            var user1 = new User() { Id = "userId1", IsMechanic = false };
            var user2 = new User() { Id = "userId2", IsMechanic = true, PhoneNumber = "", FirstName = "", LastName = "" };
            var user3 = new User() { Id = "userId3", IsMechanic = true, PhoneNumber = "", FirstName = "", LastName = "" };
            var user4 = new User() { Id = "userId4", IsMechanic = false };
            await repo.AddAsync(user1);
            await repo.AddAsync(user2);
            await repo.AddAsync(user3);
            await repo.AddAsync(user4);

            var category = new RepairCategory() { Id = 1, Name = "category" };

            await repo.AddAsync(category);

            await repo.AddRangeAsync(new List<Repair>()
            {
                new Repair(){Id = 1, Category = category, RepairCategoryId =1,  Description ="active1", Brand = "", CarModel = "", IsActive = true, IsApproved = true, IsTaken = false, Status = "Active", Owner = user1, OwnerId = user1.Id},

                new Repair(){Id = 2, Category = category, RepairCategoryId =1, Description ="active2", Brand = "", CarModel = "", IsActive = true, IsApproved = true, IsTaken = false, Status = "Active", Owner = user1, OwnerId = user1.Id},

                new Repair(){Id = 3, Category = category, RepairCategoryId =1, Description ="active2", Brand = "", CarModel = "", IsActive = true, IsApproved = true, IsTaken = false, Status = "Active", Owner = user4, OwnerId = user4.Id},

            });

            await repo.AddRangeAsync(new List<Offer>()
            {
                new Offer(){Id = 1, Description = "offer1" , IsActive = true, Owner = user2, OwnerId = user2.Id, Price = 1},

                new Offer(){Id = 2, Description = "offer2" , IsActive = true, Owner = user2, OwnerId = user2.Id, Price = 1},

                new Offer(){Id = 3, Description = "offer3" , IsActive = true, Owner = user3, OwnerId = user3.Id, Price = 1},

                 new Offer(){Id = 4, Description = "offer4" , IsActive = true, Owner = user3, OwnerId = user3.Id, Price = 1}

            });

            await repo.AddRangeAsync(new List<RepairOffer>()
            {
                new RepairOffer(){ RepairId = 1, OfferId = 1},
                new RepairOffer(){ RepairId = 2, OfferId = 2},
                new RepairOffer(){ RepairId = 1, OfferId = 3},
                new RepairOffer(){ RepairId = 3, OfferId = 4}
            });

            await repo.SaveChangesAsync();

            var threeOffersUser = await service.RepairOffersAsync(user1.Id);

            var oneOfferUser = await service.RepairOffersAsync(user4.Id);

            Assert.That(3, Is.EqualTo(threeOffersUser.Count()));
            Assert.That(threeOffersUser.ElementAt(0).Description, Is.EqualTo("offer1"));
            Assert.That(threeOffersUser.ElementAt(1).Description, Is.EqualTo("offer2"));
            Assert.That(threeOffersUser.ElementAt(2).Description, Is.EqualTo("offer3"));

            Assert.That(1, Is.EqualTo(oneOfferUser.Count()));
            Assert.That(oneOfferUser.ElementAt(0).Description, Is.EqualTo("offer4"));
        }

        [Test]
        public async Task AllCategories()
        {
            mechanicService = new MechanicService(repo);
            service = new RepairService(repo, mechanicService);

            await repo.AddRangeAsync(new List<RepairCategory>()
            {
                new RepairCategory(){Id =1 ,Name = "1"},
                new RepairCategory(){Id =2 ,Name = "2"},
                new RepairCategory(){Id =3 ,Name = "3"},
            });

            await repo.SaveChangesAsync();

            var categories = await service.AllCategories();

            Assert.That(3, Is.EqualTo(categories.Count()));
            Assert.That(categories.ElementAt(0).Name, Is.EqualTo("1"));
            Assert.That(categories.ElementAt(1).Name, Is.EqualTo("2"));
            Assert.That(categories.ElementAt(2).Name, Is.EqualTo("3"));
        }


        [Test]
        public async Task CategoryExists()
        {
            mechanicService = new MechanicService(repo);
            service = new RepairService(repo, mechanicService);

            await repo.AddRangeAsync(new List<RepairCategory>()
            {
                new RepairCategory(){Id =1 ,Name = "1"}
            });

            await repo.SaveChangesAsync();

            var firstCategory = await service.CategoryExists(1);
            var secondCategory = await service.CategoryExists(2);

            Assert.IsTrue(firstCategory);
            Assert.IsFalse(secondCategory);
        }

        [Test]
        public async Task GetMyRepairsAsync()
        {
            mechanicService = new MechanicService(repo);
            service = new RepairService(repo, mechanicService);

            var user1 = new User() { Id = "userId1", IsMechanic = false };
            var user2 = new User() { Id = "userId2", IsMechanic = false };
            await repo.AddAsync(user1);
            await repo.AddAsync(user2);

            var category = new RepairCategory() { Id = 1, Name = "category" };

            await repo.AddAsync(category);

            await repo.AddRangeAsync(new List<Repair>()
            {
                new Repair(){Id = 1, Category = category, RepairCategoryId =1,  Description ="active1", Brand = "", CarModel = "", IsActive = true, IsApproved = true, IsTaken = false, Status = "Active", Owner = user1, OwnerId = user1.Id},

                new Repair(){Id = 2, Category = category, RepairCategoryId =1, Description ="taken", Brand = "", CarModel = "", IsActive = true, IsApproved = true, IsTaken = true, Status = "Taken", Owner = user1, OwnerId = user1.Id},

                new Repair(){Id = 3, Category = category, RepairCategoryId =1, Description ="active2", Brand = "", CarModel = "", IsActive = true, IsApproved = true, IsTaken = false, Status = "Active", Owner = user1, OwnerId = user1.Id},

                new Repair(){Id = 4, Category = category, RepairCategoryId =1, Description ="pending" ,Brand = "", CarModel = "", IsActive = true, IsApproved = false, IsTaken = false, Status = "Pending", Owner = user2, OwnerId = user2.Id},

                new Repair(){Id = 5, Category = category, RepairCategoryId =1, Description ="removed", Brand = "", CarModel = "", IsActive = false, IsApproved = true, IsTaken = false, Status = "Removed", Owner = user2, OwnerId = user2.Id}
            });
            await repo.SaveChangesAsync();

            var user1Repairs = await service.GetMyRepairsAsync(user1.Id);
            var user2Repairs = await service.GetMyRepairsAsync(user2.Id);

            Assert.That(3, Is.EqualTo(user1Repairs.Count()));
            Assert.That(user1Repairs.Any(h => h.Id == 4), Is.False);
            Assert.That(user1Repairs.Any(h => h.Id == 5), Is.False);
            Assert.That(1, Is.EqualTo(user2Repairs.Count()));
            Assert.That(user2Repairs.Any(h => h.Id == 5), Is.False);

        }

        [Test]
        public async Task CompleteRepair()
        {
            mechanicService = new MechanicService(repo);
            service = new RepairService(repo, mechanicService);

            var user1 = new User() { Id = "userId1", IsMechanic = false };
            var user2 = new User() { Id = "userId2", IsMechanic = true };
            await repo.AddAsync(user1);
            await repo.AddAsync(user2);

            var category = new RepairCategory() { Id = 1, Name = "category" };

            await repo.AddAsync(category);

            await repo.AddAsync(new Repair()
            {
                Id = 1,
                Category = category,
                RepairCategoryId = 1,
                Description = "active1",
                Brand = "",
                CarModel = "",
                IsActive = true,
                IsApproved = true,
                IsTaken = true,
                Status = "Active",
                Owner = user1,
                OwnerId = user1.Id,
                MechanicId = user2.Id
            });
            await repo.SaveChangesAsync();

            string mechanicId = await service.CompleteRepair(1, "userId1");
            var repair = await repo.AllReadonly<Repair>().Where(x => x.Id == 1).FirstAsync();

            Assert.IsNotNull(repair);
            Assert.That(repair.Status, Is.EqualTo("Completed"));
            Assert.That(repair.IsActive, Is.False);
            Assert.That(mechanicId, Is.EqualTo("userId2"));
        }

        [Test]
        public async Task CompleteRepairThrowsException()
        {
            mechanicService = new MechanicService(repo);
            service = new RepairService(repo, mechanicService);

            var user1 = new User() { Id = "userId1", IsMechanic = false };
            var user2 = new User() { Id = "userId2", IsMechanic = true };
            await repo.AddAsync(user1);
            await repo.AddAsync(user2);

            var category = new RepairCategory() { Id = 1, Name = "category" };

            await repo.AddAsync(category);

            await repo.AddRangeAsync(new List<Repair>(){
                new Repair()
                {
                Id = 1,
                Category = category,
                RepairCategoryId = 1,
                Description = "active1",
                Brand = "",
                CarModel = "",
                IsActive = true,
                IsApproved = true,
                IsTaken = true,
                Status = "Active",
                Owner = user1,
                OwnerId = user1.Id,
                MechanicId = user2.Id
                },
                new Repair()
                {
                Id = 2,
                Category = category,
                RepairCategoryId = 1,
                Description = "active1",
                Brand = "",
                CarModel = "",
                IsActive = true,
                IsApproved = true,
                IsTaken = false,
                Status = "Active",
                Owner = user1,
                OwnerId = user1.Id,
                MechanicId = user2.Id
                },
                new Repair()
                {
                Id = 3,
                Category = category,
                RepairCategoryId = 1,
                Description = "active1",
                Brand = "",
                CarModel = "",
                IsActive = true,
                IsApproved = true,
                IsTaken = true,
                Status = "Active",
                Owner = user1,
                OwnerId = user1.Id,
                }

            });
            await repo.SaveChangesAsync();

            Assert.That(async () => await service.CompleteRepair(4, "userId1"),
               Throws.Exception.With.Property("Message")
               .EqualTo("Repair not found"));


            Assert.That(async () => await service.CompleteRepair(2, "userId1"),
               Throws.Exception.With.Property("Message")
               .EqualTo("Repair is not taken"));


            Assert.That(async () => await service.CompleteRepair(3, "userId1"),
               Throws.Exception.With.Property("Message")
               .EqualTo("Repair is not taken"));


            Assert.That(async () => await service.CompleteRepair(1, "invalidId"),
               Throws.Exception.With.Property("Message")
               .EqualTo("Invalid user Id"));
        }

        [Test]
        public async Task DeleteRepairAsync()
        {
            mechanicService = new MechanicService(repo);
            service = new RepairService(repo, mechanicService);

            var user1 = new User() { Id = "userId1", IsMechanic = false };
            await repo.AddAsync(user1);

            var user2 = new User()
            {
                Id = "userId2",
                IsMechanic = true,
                FirstName = "",
                LastName = "",
                PhoneNumber = ""
            };
            await repo.AddAsync(user2);

            var category = new RepairCategory() { Id = 1, Name = "category" };
            await repo.AddAsync(category);
            await repo.AddAsync(new Repair()
            {
                Id = 1,
                Category = category,
                RepairCategoryId = 1,
                Description = "active1",
                Brand = "",
                CarModel = "",
                IsActive = true,
                IsApproved = true,
                IsTaken = false,
                Status = "Active",
                Owner = user1,
                OwnerId = user1.Id,
            });

            await repo.AddAsync(new Offer()
            {
                Description = "",
                Id = 1,
                IsActive = true,
                Owner = user2,
                OwnerId = user2.Id,
                Price = 1,
                IsAccepted = false
            });

            await repo.AddAsync(new RepairOffer() { RepairId = 1, OfferId = 1 });

            await repo.SaveChangesAsync();

            await service.DeleteRepairAsync(1, user1.Id);

            var repair = await repo.AllReadonly<Repair>().Where(x => x.Id == 1).FirstOrDefaultAsync();
            var offer = await repo.AllReadonly<Offer>().Where(x => x.Id == 1).FirstOrDefaultAsync();

            Assert.IsNotNull(repair);
            Assert.IsNotNull(offer);
            //Assert.That(repair.Status, Is.EqualTo("Deleted"));
            Assert.That(repair.IsActive, Is.False);
            Assert.That(offer.IsActive, Is.False);
        }

        [Test]
        public async Task DeleteRepairAsyncThrowsException()
        {
            mechanicService = new MechanicService(repo);
            service = new RepairService(repo, mechanicService);

            var user1 = new User() { Id = "userId1", IsMechanic = false };
            await repo.AddAsync(user1);

            var category = new RepairCategory() { Id = 1, Name = "category" };
            await repo.AddAsync(category);
            await repo.AddAsync(new Repair()
            {
                Id = 1,
                Category = category,
                RepairCategoryId = 1,
                Description = "active1",
                Brand = "",
                CarModel = "",
                IsActive = true,
                IsApproved = true,
                IsTaken = true,
                Status = "Active",
                Owner = user1,
                OwnerId = user1.Id,
                MechanicId = ""
            });

            await repo.AddAsync(new Repair()
            {
                Id = 2,
                Category = category,
                RepairCategoryId = 1,
                Description = "active1",
                Brand = "",
                CarModel = "",
                IsActive = true,
                IsApproved = false,
                IsTaken = false,
                Status = "Pending",
                Owner = user1,
                OwnerId = user1.Id,
            });


            await repo.AddAsync(new Repair()
            {
                Id = 3,
                Category = category,
                RepairCategoryId = 1,
                Description = "active1",
                Brand = "",
                CarModel = "",
                IsActive = true,
                IsApproved = true,
                IsTaken = false,
                Status = "Active",
                Owner = user1,
                OwnerId = user1.Id,
            });

            await repo.SaveChangesAsync();


            Assert.That(async () => await service.DeleteRepairAsync(101, user1.Id),
               Throws.Exception.With.Property("Message")
               .EqualTo("Repair not found"));


            Assert.That(async () => await service.DeleteRepairAsync(2, user1.Id),
               Throws.Exception.With.Property("Message")
               .EqualTo("Repair not reviewed"));

            Assert.That(async () => await service.DeleteRepairAsync(1, user1.Id),
              Throws.Exception.With.Property("Message")
              .EqualTo("Can't delete ongoing repair"));


            Assert.That(async () => await service.DeleteRepairAsync(3, "invalidId"),
              Throws.Exception.With.Property("Message")
              .EqualTo("User is not owner"));
        }


        [TearDown]
        public void TearDown()
        {
            context.Dispose();
        }
    }
}
