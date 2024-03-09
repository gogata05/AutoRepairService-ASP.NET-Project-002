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

        public async Task AddRepairAsync(string id, RepairModel model)
        {
            var user = await repo.GetByIdAsync<User>(id);
            var repair = new Repair()
            {
                Brand = model.Brand,
                Model = model.Model,
                Description = model.Description,
                Category = model.Category,
                OwnerName = user.UserName,
                OwnerId = user.Id,
                StartDate = DateTime.Now,
            };
            await repo.AddAsync<Repair>(repair);
            await repo.SaveChangesAsync();
        }

        public async Task<RepairModel> GetEditAsync(int id)
        {
            var repair = await repo.GetByIdAsync<Repair>(id);

            if (repair == null)
            {
                throw new Exception("REPAIR NOT FOUND");
            }

            //string userId = ??;
            //if (userId != task.OwnerId)
            //{
            //    return Unauthorized();
            //}

            var model = new RepairModel()
            {
                Brand = repair.Brand,
                Model = repair.Model,
                Description = repair.Description,
                Category = repair.Category
            };

            return model;
        }

        public async Task PostEditAsync(int id, RepairModel model)
        {
            var repair = await repo.GetByIdAsync<Repair>(id);

            if (repair == null)
            {
                throw new Exception("REPAIR NOT FOUND");
            }

            repair.Brand = model.Brand;
            repair.Model = model.Model;
            repair.Description = model.Description;
            repair.Category = model.Category;

            await repo.SaveChangesAsync();
        }

        public async Task<IEnumerable<RepairViewModel>> GetAllRepairsAsync() // all open repairs
        {
            var repairs = await repo.AllReadonly<Repair>().Where(j => j.IsTaken == false).ToListAsync();

            return repairs
                .Select(j => new RepairViewModel()
                {
                    Id = j.Id,
                    Brand = j.Brand,
                    Model = j.Model,
                    Category = j.Category,
                    Description = j.Description,
                    OwnerName = j.OwnerName ?? "No name",
                    OwnerId = j.OwnerId,
                    StartDate = j.StartDate
                });

        }

        public async Task<RepairViewModel> RepairDetailsAsync(int id)
        {
            var repair = await repo.GetByIdAsync<Repair>(id);
            var model = new RepairViewModel()
            {
                OwnerId = repair.OwnerId,
                Model = repair.Model,
                Brand = repair.Brand,
                Description = repair.Description,
                Category = repair.Category,
            };

            return model;

        }

    }
}
