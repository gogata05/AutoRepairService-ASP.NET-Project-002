using AutoRepairService.Core.ViewModels.Car;

namespace AutoRepairService.Core.IServices
{
    public interface ICarService
    {
        Task<IEnumerable<CarViewModel>> GetAllCarsAsync();

        Task<IEnumerable<string>> AllCategoriesNames();

        Task<AllCarsQueryModel> AllCarsAsync(
            string? category = null,
            string? searchTerm = null,
            CarSorting sorting = CarSorting.Newest,
            int currentPage = 1,
            int carsPerPage = 1);

        Task<IEnumerable<CarServiceViewModel>> GetLastThreeCars();
    }
}
