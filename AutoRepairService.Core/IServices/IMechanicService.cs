using AutoRepairService.Core.ViewModels.Mechanic;

namespace AutoRepairService.Core.IServices
{
    public interface IMechanicService
    {
        Task AddMechanicAsync(string id, BecomeMechanicViewModel model);

        Task<IEnumerable<MechanicViewModel>> AllMechanicsAsync();

        Task<string> MechanicRatingAsync(string mechanicId);

        public Task RateMechanicAsync(string userId, string mechanicId, int repairId, MechanicRatingModel model);

    }
}
