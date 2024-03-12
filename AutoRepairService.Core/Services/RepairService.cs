using AutoRepairService.Core.IServices;
using AutoRepairService.Core.ViewModels;
using AutoRepairService.Core.ViewModels.Offer;
using AutoRepairService.Infrastructure.Data.Common;
using AutoRepairService.Infrastructure.Data.EntityModels;
using Microsoft.EntityFrameworkCore;

namespace AutoRepairService.Core.Services
{
    public class RepairService : IRepairService
    {
        private readonly IRepository repo;
        private readonly IMechanicService mechanicService;

        public RepairService(IRepository _repo, IMechanicService _mechanicService)
        {
            repo = _repo;
            mechanicService = _mechanicService;
        }

        public async Task AddRepairAsync(string id, RepairModel model)
        {
            var user = await repo.All<User>().Where(x => x.Id == id).FirstOrDefaultAsync();

            if (user == null)
            {
                throw new Exception("User not found");
            }

            var repair = new Repair()
            {
                Brand = model.Brand,
                CarModel = model.CarModel,
                Description = model.Description,
                RepairCategoryId = model.CategoryId,
                OwnerName = user.UserName,
                Owner = user,
                OwnerId = user.Id,
                StartDate = DateTime.Now,
                IsActive = true
            };
            await repo.AddAsync<Repair>(repair);
            await repo.SaveChangesAsync();
        }
       
        public async Task<IEnumerable<RepairViewModel>> GetAllRepairsAsync()
        {
            var repairs = await repo.AllReadonly<Repair>().Where(j => j.IsTaken == false && j.IsApproved == true && j.IsActive == true && j.Status == "Active").Include(j => j.Category).ToListAsync();

            if (repairs == null)
            {
                throw new Exception("Repair entity error");
            }

            return repairs
                .Select(j => new RepairViewModel()
                {
                    Id = j.Id,
                    Brand = j.Brand,
                    CarModel = j.CarModel,
                    Category = j.Category.Name,
                    Description = j.Description,
                    OwnerName = j.OwnerName,
                    OwnerId = j.OwnerId,
                    StartDate = j.StartDate
                });
        }
        
        public async Task<RepairModel> GetEditAsync(int id, string userId)
        {
            if (!await RepairExistAsync(id))
            {
                throw new Exception("Repair not found");
            }

            var owner = await repo.AllReadonly<User>().Where(x => x.Id == userId).FirstOrDefaultAsync();

            if (owner == null)
            {
                throw new Exception("Owner not found");
            }

            var repair = await repo.All<Repair>().Where(x => x.Id == id).Include(x => x.Owner).FirstOrDefaultAsync();

            if (repair.Owner?.Id != userId)
            {
                throw new Exception("User is not owner");
            }


            if (repair.IsTaken == true)
            {
                throw new Exception("Can't edit ongoing repair");
            }


            if (repair.IsApproved != true)
            {
                throw new Exception("This repair is not approved");
            }

            var model = new RepairModel()
            {
                Brand = repair.Brand,
                CarModel = repair.CarModel,
                Description = repair.Description,
                CategoryId = repair.RepairCategoryId,
                Owner = owner,
                OwnerName = repair.OwnerName
            };
            return model;
        }
      
        public async Task PostEditAsync(int id, RepairModel model)
        {
            if (!await RepairExistAsync(id))
            {
                throw new Exception("Repair not found");
            }

            var repair = await repo.All<Repair>().Where(x => x.Id == id).Include(x => x.Owner).FirstOrDefaultAsync();

            repair.Brand = model.Brand;
            repair.CarModel = model.CarModel;
            repair.Description = model.Description;
            repair.RepairCategoryId = model.CategoryId;

            await repo.SaveChangesAsync();
        }
       
        public async Task<RepairViewModel> RepairDetailsAsync(int id)
        {
            if (!await RepairExistAsync(id))
            {
                throw new Exception("Repair not found");
            }

            var repair = await repo.AllReadonly<Repair>()
                .Where(j => j.Id == id)
                .Include(c => c.Category)
                .FirstAsync();


            var model = new RepairViewModel()
            {
                OwnerId = repair.OwnerId,
                OwnerName = repair.OwnerName,
                Brand = repair.Brand,
                CarModel = repair.CarModel,
                Description = repair.Description,
                Category = repair.Category.Name,
                Id = repair.Id
            };

            return model;

        }
      
        public async Task<bool> RepairExistAsync(int id)
        {
            var result = await repo.AllReadonly<Repair>().Where(x => x.Id == id).AnyAsync();

            return result;
        }
       
        public async Task<IEnumerable<OfferServiceViewModel>> RepairOffersAsync(string userId)
        {
            var repairOffers = await repo.AllReadonly<RepairOffer>()
                .Where(x => x.Repair.OwnerId == userId && x.Offer.IsAccepted == null
                && x.Repair.IsTaken == false && x.Offer.IsActive == true && x.Repair.IsActive == true).Include(j => j.Repair).Include(c => c.Repair.Category).Include(o => o.Offer).Include(u => u.Offer.Owner).ToListAsync();

            if (repairOffers == null)
            {
                throw new Exception("RepairOffer entity error");
            }

            List<OfferServiceViewModel> offers = new List<OfferServiceViewModel>();

            foreach (var x in repairOffers)
            {
                offers.Add(new OfferServiceViewModel()
                {
                    Id = x.OfferId,
                    Description = x.Offer.Description,
                    RepairDescription = x.Repair.Description,
                    MechanicName = $"{x.Offer.Owner.FirstName} {x.Offer.Owner.LastName}",
                    MechanicPhoneNumber = x.Offer.Owner.PhoneNumber,
                    RepairId = x.RepairId,
                    RepairBrand = x.Repair.Brand,
                    RepairModel = x.Repair.CarModel,
                    RepairCategory = x.Repair.Category.Name,
                    OwnerId = x.Offer.OwnerId,
                    Rating = await mechanicService.MechanicRatingAsync(x.Offer.OwnerId),
                    Price = x.Offer.Price
                });
            }
            return offers;
        }

        public async Task<IEnumerable<CategoryViewModel>> AllCategories()
        {
            return await repo.AllReadonly<RepairCategory>()
                .Select(x => new CategoryViewModel()
                {
                    Id = x.Id,
                    Name = x.Name
                })
                .ToListAsync();
        }
        /// <summary>
        /// Returns bool for category pressent in DB
        /// </summary>
        /// <param name="categoryId"></param>
        /// <returns></returns>
        public async Task<bool> CategoryExists(int categoryId) // check if needed
        {
            return await repo.AllReadonly<RepairCategory>()
                .AnyAsync(c => c.Id == categoryId);
        }

        public async Task<IEnumerable<MyRepairViewModel>> GetMyRepairsAsync(string userId)
        {
            var myRepairs = await repo.AllReadonly<Repair>()
                .Where(j => j.OwnerId == userId && j.IsActive == true)
                .Include(j => j.Category)
                .ToListAsync();

            if (myRepairs == null)
            {
                throw new Exception("Repair entity error");
            }

            return myRepairs
                .Select(j => new MyRepairViewModel()
                {
                    Id = j.Id,
                    OwnerId = j.OwnerId,
                    Brand = j.Brand,
                    Category = j.Category.Name,
                    Description = j.Description,
                    IsTaken = j.IsTaken,
                    IsActive = j.IsActive,
                    IsApproved = j.IsApproved,
                    MechanicId = j.MechanicId,
                    EndDate = j.EndDate,
                    StartDate = j.StartDate,
                    Status = j.Status
                });
        }
      
        public async Task<string> CompleteRepair(int repairId, string userId)
        {
            var repair = await repo.All<Repair>().Where(x => x.Id == repairId).FirstOrDefaultAsync();

            if (repair == null)
            {
                throw new Exception("Repair not found");
            }

            if (repair.IsTaken == false || repair.MechanicId == null)
            {
                throw new Exception("Repair is not taken");
            }

            if (repair.OwnerId != userId)
            {
                throw new Exception("Invalid user Id");
            }

            var mechanicId = repair.MechanicId;

            repair.IsActive = false;
            repair.EndDate = DateTime.Now;
            repair.Status = "Completed";
            await repo.SaveChangesAsync();

            return mechanicId;
        }
        
        public async Task DeleteRepairAsync(int repairId, string userId)
        {
            var repair = await repo.All<Repair>().Where(x => x.Id == repairId).FirstOrDefaultAsync();

            if (repair == null)
            {
                throw new Exception("Repair not found");
            }

            if (repair.IsApproved == false)
            {
                throw new Exception("Repair not reviewed");

            }

            if (repair.IsTaken == true)
            {
                throw new Exception("Can't delete ongoing repair");
            }


            if (repair.OwnerId != userId)
            {
                throw new Exception("User is not owner");
            }

            var repairOffer = await repo.All<RepairOffer>().Where(x => x.RepairId == repairId).ToListAsync();

            if (repairOffer != null && repairOffer.Count > 0)
            {
                foreach (var jo in repairOffer)
                {
                    var offer = await repo.GetByIdAsync<Offer>(jo.OfferId);
                    offer.IsActive = false;
                }
            }
            repair.IsActive = false;
            repair.Status = "Deleted";
            await repo.SaveChangesAsync();
        }
    }
}
