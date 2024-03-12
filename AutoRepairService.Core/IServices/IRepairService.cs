
using AutoRepairService.Core.ViewModels;
using AutoRepairService.Core.ViewModels.Offer;

namespace AutoRepairService.Core.IServices
{
    public interface IRepairService
    {
        Task AddRepairAsync(string id, RepairModel model);

        Task<IEnumerable<RepairViewModel>> GetAllRepairsAsync();

        Task<IEnumerable<MyRepairViewModel>> GetMyRepairsAsync(string userId);

        Task<string> CompleteRepair(int repairId, string userId);

        Task<RepairModel> GetEditAsync(int id, string userId);

        Task PostEditAsync(int id, RepairModel model);

        Task<RepairViewModel> RepairDetailsAsync(int id);

        Task<bool> RepairExistAsync(int id);

        Task<IEnumerable<OfferServiceViewModel>> RepairOffersAsync(string userId);

        Task<IEnumerable<CategoryViewModel>> AllCategories();

        Task<bool> CategoryExists(int categoryId);

        Task DeleteRepairAsync(int repairId, string userId);

    }
}
