
using AutoRepairService.Core.ViewModels;

namespace AutoRepairService.Areas.Admin.IServices
{
    public interface IAdministrationService
    {
        Task AddRepairAsync(string id, RepairModel model);

        Task<IEnumerable<RepairViewModel>> GetAllRepairsAsync();

        Task<RepairModel> GetEditAsync(int id);

        Task PostEditAsync(int id, RepairModel model);

        Task<RepairViewModel> RepairDetailsAsync(int id);
    }
}
