using AutoRepairService.Core.ViewModels;
using AutoRepairService.Core.ViewModels.Admin;

namespace AutoRepairService.Core.IServices
{
    public interface IRepairAdministrationService
    {

        Task<IEnumerable<RepairViewModel>> GetAllRepairsAsync();

        Task<RepairViewModel> RepairDetailsAsync(int id);

        Task ApproveRepairAsync(int id);

        Task DeclineRepairAsync(int id);

        Task<IEnumerable<RepairViewAdminModel>> ReviewPendingRepairs();

        Task<IEnumerable<RepairViewAdminModel>> ReviewDeclinedRepairs();

        Task<IEnumerable<RepairViewAdminModel>> ReviewActiveRepairs();

        Task<RepairModel> GetEditAsync(int id);

        Task PostEditAsync(int id, RepairModel model);

    }
}
