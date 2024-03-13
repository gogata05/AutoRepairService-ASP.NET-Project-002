using AutoRepairService.Core.ViewModels;
using AutoRepairService.Core.ViewModels.Car;

namespace AutoRepairService.Core.IServices
{
    public interface IAdminCarService
    {
        Task AddCarAsync(CarModel model, string id);

        Task<IEnumerable<CategoryViewModel>> AllCategories();

        Task<bool> CategoryExists(int categoryId);

        Task<bool> CarExistAsync(int carId);

        Task<CarModel> GetEditAsync(int id, string userId);

        Task PostEditAsync(int id, CarModel model);

        Task RemoveCarAsync(int id, string userId);

    }
}
