
using AutoRepairService.Core.ViewModels;

namespace AutoRepairService.Core.IServices
{
    public interface IRepairService
    {
        Task AddJumpAsync(string id, RepairModel model);
        Task<IEnumerable<RepairViewModel>> GetAllRepairsAsync();
      
    }
}
