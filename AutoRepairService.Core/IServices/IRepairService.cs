
using AutoRepairService.Core.ViewModels;

namespace AutoRepairService.Core.IServices
{
    public interface IRepairService
    {
        Task AddRepairAsync(string id, RepairModel model);
        Task<IEnumerable<RepairViewModel>> GetAllRepairsAsync();

        Task<RepairModel> GetEditAsync(int id);

        Task PostEditAsync(int id, RepairModel model);

        Task<RepairViewModel> RepairDetailsAsync(int id);
    }
}
