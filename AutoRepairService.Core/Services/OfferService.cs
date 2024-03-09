
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

        public async Task AcceptOfferAsync(Offer offer)
        {
            offer.IsAccepted = true;
            await repo.SaveChangesAsync();
        }

        public async Task DeclineOfferAsync(Offer offer)
        {
            offer.IsAccepted = false;
            await repo.SaveChangesAsync();
        }

        public async Task<Offer> GetOfferAsync(int id)
        {
            //if (await OfferExists(id))
            //{

            //}
            return await repo.GetByIdAsync<Offer>(id);
        }

        public async Task<IEnumerable<MyOffersViewModel>> MyOffersAsync(string userId)
        {
            return await repo.AllReadonly<RepairOffer>().Where(j => j.Repair.OwnerId == userId)
                .Select(x => new MyOffersViewModel()
                {
                    Description = x.Offer.Description,
                    MechanicName = x.Offer.OwnerId,
                    OfferId = x.Offer.Id

                }).ToListAsync();



            //
            //return .Select(async x => new MyOffersViewModel()
            //{
            //    Description = x.Offer.Description,
            //    ContractorName = "Name"


            //}).ToListAsync();
        }

        public async Task<bool> OfferExists(int id)
        {
            return await repo.AllReadonly<Offer>().AnyAsync(x => x.Id == id);
        }

        public async Task<IEnumerable<MyOffersViewModel>> OffersConditionAsync(string userId)
        {
            return await repo.AllReadonly<RepairOffer>().Where(j => j.Offer.OwnerId == userId)
                .Select(x => new MyOffersViewModel()
                {
                    Description = x.Offer.Description,
                    MechanicName = x.Offer.OwnerId,
                    RepairOwnerId = x.Repair.Owner.Id,
                    RepairOwnerName = x.Repair.Owner.UserName,
                    IsAccepted = x.Offer.IsAccepted,
                    OfferId = x.Offer.Id

                }).ToListAsync();
        }

        public async Task<OfferServiceViewModel> ReviewOfferAsync(int id) // change with single return
        {   // check if offer exist

            var offer = await repo.AllReadonly<Offer>()
                .Where(x => x.Id == id)
                .Include(j => j.RepairsOffers.Where(o => o.OfferId == id))
                .Select(x => new OfferServiceViewModel()
                {
                    Id = id,
                    Price = x.Price,
                    Description = x.Description,
                    OwnerId = x.OwnerId,
                    RepairId = x.RepairsOffers.Select(x => x.RepairId).First()
                }).FirstOrDefaultAsync();

            return offer;

            //    var offer = await repo.GetByIdAsync<Offer>(id)
            // .Include(j => j.RepairsOffers.Where(o => o.OfferId == id))
            // .Select(x => new OfferViewModel()
            // {
            //     Description = x.Description,
            //     OwnerId = x.OwnerId,
            //     RepairId = x.RepairsOffers.Select(x => x.RepairId).First()
            // }).FirstOrDefaultAsync();

            //    return offer;

        }

        /*repo.AllReadonly<User>().Where(u => u.Id == x.Offer.OwnerId).Select(x => x.UserName)*/// add name to offer?
        public async Task SendOfferAsync(OfferViewModel model, int repairId, string userId)
        {

            // check model state?
            //check if offer already exist
            var repair = await repo.GetByIdAsync<Repair>(repairId);
            var user = await repo.GetByIdAsync<User>(userId);
            var offer = new Offer()
            {
                Description = model.Description,
                OwnerId = userId,
                // Owner = user,
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
