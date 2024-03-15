
using AutoRepairService.Core.IServices;
using AutoRepairService.Core.ViewModels.Offer;
using AutoRepairService.Infrastructure.Data.Common;
using AutoRepairService.Infrastructure.Data.EntityModels;
using Microsoft.EntityFrameworkCore;

namespace AutoRepairService.Core.Services
{
    public class OfferService : IOfferService
    {
        private readonly IRepository repo;

        public OfferService(IRepository _repo)
        {
            repo = _repo;
        }
       
        public async Task AcceptOfferAsync(int offerId)
        {
            if (await OfferExists(offerId))
            {
                int repairId = await repo.AllReadonly<RepairOffer>().Where(o => o.OfferId == offerId).Select(x => x.RepairId).FirstOrDefaultAsync();

                if (repairId == 0)
                {
                    throw new Exception("Repair not found");
                }

                var offer = await GetOfferAsync(offerId);
                offer.IsAccepted = true;

                var repair = await repo.GetByIdAsync<Repair>(repairId);
                repair.MechanicId = offer.OwnerId;
                repair.IsTaken = true;

                await repo.SaveChangesAsync();
            }
            else
            {
                throw new Exception("Offer don't exist");
            }
        }
        
        public async Task DeclineOfferAsync(int offerId)
        {
            if (await OfferExists(offerId))
            {
                var offer = await GetOfferAsync(offerId);
                offer.IsAccepted = false;
                await repo.SaveChangesAsync();
            }
            else
            {
                throw new Exception("Offer don't exist");
            }
        }
       
        public async Task<Offer> GetOfferAsync(int id)
        {
            var offer = await repo.All<Offer>()
                .Where(x => x.Id == id).FirstOrDefaultAsync();
            if (offer == null)
            {
                throw new Exception("Offer don't exist");
            }
            return offer;
        }
       
        public async Task<bool> OfferExists(int id)
        {
            return await repo.AllReadonly<Offer>().AnyAsync(x => x.Id == id);
        }
        
        public async Task<IEnumerable<OfferServiceViewModel>> OffersConditionAsync(string userId)
        {
            var offersCondition = await repo.AllReadonly<RepairOffer>().Where(j => j.Offer.OwnerId == userId && j.Offer.IsActive == true)
                .Select(x => new OfferServiceViewModel()
                {
                    Description = x.Offer.Description,
                    MechanicName = x.Offer.Owner.UserName,
                    RepairCategory = x.Repair.Category.Name ?? "include category!",
                    RepairBrand = x.Repair.Brand,
                    RepairModel = x.Repair.CarModel,
                    IsAccepted = x.Offer.IsAccepted,
                    Id = x.Offer.Id,
                    Price = x.Offer.Price,
                    MechanicPhoneNumber = x.Offer.Owner.PhoneNumber,
                    FirstName = x.Offer.Owner.FirstName,
                    LastName = x.Offer.Owner.LastName,
                    RepairDescription = x.Repair.Description,
                    RepairId = x.RepairId,
                    OwnerId = userId,
                    Rating = "Rating"

                }).ToListAsync();

            if (offersCondition == null)
            {
                throw new Exception("RepairOffer entity error");
            }

            return offersCondition;
        }
       
        public async Task RemoveOfferAsync(string id, int offerId)
        {
            if (!await OfferExists(offerId))
            {
                throw new Exception("Offer don't exist");
            }

            var offer = await repo.GetByIdAsync<Offer>(offerId);

            if (offer.OwnerId != id)
            {
                throw new Exception("User not owner");
            }

            if (offer.IsAccepted == true || offer.IsAccepted == null)
            {
                throw new Exception("This offer can't be deleted");
            }

            offer.IsActive = false;
            await repo.SaveChangesAsync();
        }
      
        public async Task SendOfferAsync(OfferViewModel model, int repairId, string userId)
        {
            var repair = await repo.GetByIdAsync<Repair>(repairId);
            if (repair == null)
            {
                throw new Exception("Invalid repair Id");
            }

            var userOfferExist = await repo.AllReadonly<RepairOffer>()
                .Where(x => x.Offer.OwnerId == userId
                && x.RepairId == repairId
                && x.Offer.IsAccepted != false)
                .AnyAsync();

            if (userOfferExist)
            {
                throw new Exception("One offer per repair");
            }

            var offer = new Offer()
            {
                Description = model.Description,
                OwnerId = userId,
                Price = model.Price

            };
            await repo.AddAsync<Offer>(offer);

            var repairOffer = new RepairOffer()
            {
                RepairId = repair.Id,
                Repair = repair,
                Offer = offer,
                OfferId = offer.Id
            };

            await repo.AddAsync<RepairOffer>(repairOffer);

            await repo.SaveChangesAsync();

        }
    }
}
