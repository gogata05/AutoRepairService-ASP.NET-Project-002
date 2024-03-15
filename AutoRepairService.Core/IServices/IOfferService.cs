
using AutoRepairService.Core.ViewModels.Offer;
using AutoRepairService.Infrastructure.Data.EntityModels;

namespace AutoRepairService.Core.IServices
{
    public interface IOfferService
    {
        Task SendOfferAsync(OfferViewModel model, int repairId, string userId);

        Task<IEnumerable<OfferServiceViewModel>> OffersConditionAsync(string userId);

        Task<bool> OfferExists(int id);

        Task<Offer> GetOfferAsync(int id);

        Task AcceptOfferAsync(int offerId);

        Task DeclineOfferAsync(int offerId);

        Task RemoveOfferAsync(string id, int offerId);
    }
}
