
using AutoRepairService.Areas.Admin.IServices;
using AutoRepairService.Core.ViewModels;
using AutoRepairService.Infrastructure.Data.Common;

namespace AutoRepairService.Areas.Admin.Service
{
    public class AdministrationService : IAdministrationService
    {
        private readonly IRepository repo;

        public AdministrationService(IRepository _repo)
        {
            repo = _repo;
        }

        public Task AddRepairAsync(string id, RepairModel model)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<RepairViewModel>> GetAllRepairsAsync()
        {
            throw new NotImplementedException();
        }

        public Task<RepairModel> GetEditAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<RepairViewModel> RepairDetailsAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task PostEditAsync(int id, RepairModel model)
        {
            throw new NotImplementedException();
        }
    }
}
