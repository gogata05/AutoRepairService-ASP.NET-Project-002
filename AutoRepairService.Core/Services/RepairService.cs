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
                Name = model.Title,
                Description = model.Description,
                Category = model.Category,
                Owner = user,
                OwnerId = user.Id,
                StartDate = DateTime.Now,
            };
            await repo.AddAsync(repair);
            await repo.SaveChangesAsync();
        }
        public async Task<IEnumerable<RepairViewModel>> GetAllJobsAsync()
        {
            var jobs = await repo.AllReadonly<Repair>().ToListAsync();

            return jobs
                .Select(j => new RepairViewModel()
                {
                    Id = j.Id,
                    Title = j.Name,
                    Category = j.Category,
                    Description = j.Description,
                    OwnerName = j.Owner?.UserName ?? "No Name",
                    StartDate = j.StartDate
                });
        }
        //  public async Task AddJumpAsync(string id, JumpModel model)
        // {
        //     var user = await repo.All<User>().Where(x => x.Id == id).FirstOrDefaultAsync();

        //     if (user == null)
        //     {
        //         throw new Exception("User not found");
        //     }

        //     var jump = new Jump()
        //     {
        //         Title = model.Title, 
        //         Description = model.Description, 
        //         JumpCategoryId = model.CategoryId, 
        //         OwnerName = user.UserName, 
        //         Owner = user,
        //         OwnerId = user.Id, 
        //         StartDate = DateTime.Now, 
        //         IsActive = true
        //     };
        //     await repo.AddAsync<Jump>(jump);
        //     await repo.SaveChangesAsync();
        // }
    }
}
