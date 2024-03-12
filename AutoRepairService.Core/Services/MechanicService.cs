
using AutoRepairService.Core.IServices;
using AutoRepairService.Core.ViewModels.Mechanic;
using AutoRepairService.Infrastructure.Data.Common;
using AutoRepairService.Infrastructure.Data.EntityModels;
using Microsoft.EntityFrameworkCore;

namespace AutoRepairService.Core.Services
{
    public class MechanicService : IMechanicService
    {
        private readonly IRepository repo;

        public MechanicService(IRepository _repo)
        {
            repo = _repo;
        }
        
        public async Task AddMechanicAsync(string id, BecomeMechanicViewModel model)
        {
            var user = await repo.All<User>().Where(x => x.Id == id).FirstOrDefaultAsync();

            if (user == null)
            {
                throw new Exception("User not found!");
            }

            user.PhoneNumber = model.PhoneNumber;
            user.FirstName = model.FirstName;
            user.LastName = model.LastName;
            user.IsMechanic = true;
            await repo.SaveChangesAsync();
        }
      
        public async Task<IEnumerable<MechanicViewModel>> AllMechanicsAsync()
        {
            var mechanics = await repo.AllReadonly<User>()
                .Where(x => x.IsMechanic == true
                && x.PhoneNumber != null
                && x.FirstName != null
                && x.LastName != null)
                .ToListAsync();

            if (mechanics == null)
            {
                throw new Exception("Mechanic entity error");
            }

            List<MechanicViewModel> result = new List<MechanicViewModel>();

            foreach (var mechanic in mechanics)
            {
                var data = new MechanicViewModel()
                {
                    Id = mechanic.Id,
                    FirstName = mechanic.FirstName ?? "First name",
                    LastName = mechanic.LastName ?? "Last name",
                    PhoneNumber = mechanic.PhoneNumber,
                    Rating = await MechanicRatingAsync(mechanic.Id)
                };
                result.Add(data);
            }

            return result;
        }
       
        public async Task<string> MechanicRatingAsync(string mechanicId)
        {
            double allPoints = 0;

            int ratesCount = 0;

            var rates = await repo.AllReadonly<Rating>().Where(x => x.MechanicId == mechanicId).ToListAsync();

            if (rates == null)
            {
                throw new Exception("Rating entity error");
            }

            if (rates.Count > 0)
            {
                foreach (var rate in rates)
                {
                    allPoints += rate.Points;
                    ratesCount++;
                }

                return $"{(double)allPoints / ratesCount:F2} / 5 ({ratesCount} completed repairs)";
            }

            return $"Not rated";

        }
      
        public async Task RateMechanicAsync(string userId, string mechanicId, int repairId, MechanicRatingModel model)
        {
            var user = await repo.AllReadonly<User>()
                .Where(x => x.Id == userId).AnyAsync();

            var mechanic = await repo.AllReadonly<User>()
                .Where(x => x.Id == mechanicId).AnyAsync();

            if (userId == mechanicId)
            {
                throw new Exception("You can't rate yourself!");
            }

            if (!user || !mechanic)
            {
                throw new Exception("Invalid user Id");
            }

            var repairExist = await repo.AllReadonly<Repair>()
                .Where(x => x.Id == repairId)
                .AnyAsync();

            if (!repairExist)
            {
                throw new Exception("Repair don't exist!");
            }

            var ratingExist = await repo.AllReadonly<Rating>()
                .Where(x => x.RepairId == repairId && x.UserId == userId && x.MechanicId == mechanicId)
                .AnyAsync();

            if (ratingExist)
            {
                throw new Exception("Repair is already rated!");
            }

            var mechanicRating = new Rating()
            {
                Comment = model.Comment,
                MechanicId = model.MechanicId,
                UserId = model.UserId,
                Points = model.Points,
                RepairId = model.RepairId,
            };

            await repo.AddAsync(mechanicRating);
            await repo.SaveChangesAsync();
        }
    }
}
