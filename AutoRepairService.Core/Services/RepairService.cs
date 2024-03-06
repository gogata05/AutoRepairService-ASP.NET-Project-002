using AutoRepairService.Core.IServices;
using AutoRepairService.Core.ViewModels;
using AutoRepairService.Infrastructure.Data.Common;
using AutoRepairService.Infrastructure.Data.EntityModels;
using Microsoft.EntityFrameworkCore;

namespace AutoRepairService.Core.Services
{
    public class RepairService : IRepairService
    {
        private readonly IRepository repo;
        public RepairService(IRepository _repo)
        {
            repo = _repo;
        }

        public async Task AddRepairAsync(AddRepairViewModel model)
        {
            var user = await repo.GetByIdAsync<User>("ed630639-ced3-4c6a-90cb-ad0603394d22");
            var repair = new Repair()
            {
                Brand = model.Brand,
                Model = model.Model,
                //Mileage = model.Mileage,
                Description = model.Description,
                Category = model.Category,
                Owner = user,
                OwnerId = user.Id,
                StartDate = DateTime.Now,
            };
            await repo.AddAsync(repair);
            await repo.SaveChangesAsync();
        }
        public async Task<IEnumerable<RepairViewModel>> GetAllRepairsAsync()
        {
            var repairs = await repo.AllReadonly<Repair>().ToListAsync();

            return repairs
                .Select(j => new RepairViewModel()
                {
                    Id = j.Id,
                    Brand = j.Brand,
                    Model = j.Model,
                    //Mileage = j.Mileage,
                    //Year = j.Year,
                    Category = j.Category,
                    Description = j.Description,
                    OwnerName = j.Owner?.UserName ?? "No Name",
                    StartDate = j.StartDate
                });
        }
    }
}
