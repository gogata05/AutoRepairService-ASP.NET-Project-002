
using AutoRepairService.Core.ViewModels.Offer;
using AutoRepairService.Infrastructure.Data.EntityModels;

namespace AutoRepairService.Core.IServices
{
    public interface IOfferService
    {
        Task SendOfferAsync(OfferViewModel model, int jobId, string userId);

        Task<IEnumerable<MyOffersViewModel>> MyOffersAsync(string userId);

        Task<IEnumerable<MyOffersViewModel>> OffersConditionAsync(string userId);

        Task<OfferServiceViewModel> ReviewOfferAsync(int id);

        Task<bool> OfferExists(int id);

        Task<Offer> GetOfferAsync(int id);

        Task AcceptOfferAsync(Offer offer);

        Task DeclineOfferAsync(Offer offer);
    }
}
