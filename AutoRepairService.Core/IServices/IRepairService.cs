
using AutoRepairService.Core.ViewModels;

namespace AutoRepairService.Core.IServices
{
    public interface IRepairService
    {
        Task AddRepairAsync(AddRepairViewModel model);
        Task<IEnumerable<RepairViewModel>> GetAllRepairsAsync();
      
    }
}
