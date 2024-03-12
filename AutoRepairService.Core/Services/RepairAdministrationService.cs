
using AutoRepairService.Core.IServices;
using AutoRepairService.Core.ViewModels;
using AutoRepairService.Core.ViewModels.Admin;
using AutoRepairService.Infrastructure.Data.Common;
using AutoRepairService.Infrastructure.Data.EntityModels;
using Microsoft.EntityFrameworkCore;

namespace AutoRepairService.Core.Services
{
    public class RepairAdministrationService : IRepairAdministrationService
    {
        private readonly IRepository repo;

        public RepairAdministrationService(IRepository _repo)
        {
            repo = _repo;
        }

        public Task AddRepairAsync(string id, RepairModel model)
        {
            throw new NotImplementedException();
        }

        public async Task ApproveRepairAsync(int id)
        {
            var repair = await repo.GetByIdAsync<Repair>(id);
            repair.IsApproved = true;
            repair.IsActive = true;
            repair.Status = "Active";
            repair.RepairStatusId = 2;
            await repo.SaveChangesAsync();

        }

        public async Task DeclineRepairAsync(int id)
        {
            var repair = await repo.GetByIdAsync<Repair>(id);
            repair.IsApproved = false;
            repair.IsActive = false;
            repair.Status = "Declined";
            repair.RepairStatusId = 3;
            await repo.SaveChangesAsync();
        }

        public async Task<IEnumerable<RepairViewModel>> GetAllRepairsAsync()
        {
            return await repo.All<Repair>().Select(x => new RepairViewModel()
            {
                Id = x.Id,
                //Category = x.Category,
                Description = x.Description,
                OwnerId = x.OwnerId,
                OwnerName = x.OwnerName, // change nullable!!!
                StartDate = x.StartDate,
                Brand = x.Brand,
                CarModel = x.CarModel

            }).ToListAsync();
        }

        public Task<RepairModel> GetEditAsync(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<RepairViewModel> RepairDetailsAsync(int id)
        {
            var repair = await repo.GetByIdAsync<Repair>(id);
            var model = new RepairViewModel()
            {
                OwnerId = repair.OwnerId,
                OwnerName = repair.OwnerName,
                Brand = repair.Brand,
                CarModel = repair.CarModel,
                Description = repair.Description,
                //Category = repair.Category,
                Id = repair.Id,
                StartDate = repair.StartDate,


            };

            return model;
        }

        public Task PostEditAsync(int id, RepairModel model)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<RepairViewAdminModel>> ReviewPendingRepairs()
        {
            var result = await repo.All<Repair>().Where(j => j.IsApproved == false && j.RepairStatusId == 1).Select(j => new RepairViewAdminModel()
            {
                //Category = j.Category,
                MechanicId = j.MechanicId,
                Description = j.Description,
                Id = j.Id,
                OwnerId = j.OwnerId,
                OwnerName = j.OwnerName,
                Brand = j.Brand,
                CarModel = j.CarModel,
                StartDate = j.StartDate
            }).ToListAsync();

            return result;
        }
        public async Task<IEnumerable<RepairViewAdminModel>> ReviewDeclinedRepairs()
        {
            var result = await repo.All<Repair>().Where(j => j.IsApproved == false && j.RepairStatusId == 3 && j.IsActive == false).Select(j => new RepairViewAdminModel()
            {
                //Category = j.Category,
                MechanicId = j.MechanicId,
                Description = j.Description,
                Id = j.Id,
                OwnerId = j.OwnerId,
                OwnerName = j.OwnerName,
                Brand = j.Brand,
                CarModel = j.CarModel,
                StartDate = j.StartDate
            }).ToListAsync();

            return result;
        }

        public async Task<IEnumerable<RepairViewAdminModel>> ReviewActiveRepairs()
        {
            var result = await repo.All<Repair>().Where(j => j.IsApproved == true && j.RepairStatusId == 2 && j.IsActive == true).Select(j => new RepairViewAdminModel()
            {
                //Category = j.Category,
                MechanicId = j.MechanicId,
                Description = j.Description,
                Id = j.Id,
                OwnerId = j.OwnerId,
                OwnerName = j.OwnerName,
                Brand = j.Brand,
                CarModel = j.CarModel,
                StartDate = j.StartDate
            }).ToListAsync();

            return result;
        }
    }
}
