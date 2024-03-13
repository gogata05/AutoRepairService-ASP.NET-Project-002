using Microsoft.EntityFrameworkCore;
using AutoRepairService.Core.IServices;
using AutoRepairService.Core.ViewModels.Car;
using AutoRepairService.Infrastructure.Data.Common;
using AutoRepairService.Infrastructure.Data.EntityModels;

namespace AutoRepairService.Core.Services
{
    public class CarService : ICarService
    {
        private readonly IRepository repo;

        public CarService(IRepository _repo)
        {
            repo = _repo;
        }

        public async Task<AllCarsQueryModel> AllCarsAsync(string? category = null, string? searchTerm = null, CarSorting sorting = CarSorting.Newest, int currentPage = 1, int carsPerPage = 1)
        {
            var result = new AllCarsQueryModel();
            var cars = repo.AllReadonly<Car>()
                .Where(t => t.IsActive);

            if (string.IsNullOrEmpty(category) == false)
            {
                cars = cars
                    .Where(t => t.Category.Name == category);
            }

            if (string.IsNullOrEmpty(searchTerm) == false)
            {
                searchTerm = $"%{searchTerm.ToLower()}%";

                cars = cars
                    .Where(t => EF.Functions.Like(t.Brand.ToLower(), searchTerm) ||
                        EF.Functions.Like(t.ModelOfCar.ToLower(), searchTerm) ||
                        EF.Functions.Like(t.Description.ToLower(), searchTerm));//check if correct?
            }

            cars = sorting switch
            {
                //CarSorting.NotRentedFirst => houses
                //    .OrderBy(h => h.RenterId),


                CarSorting.Price => cars
                    .OrderBy(t => t.Price),
                _ => cars.OrderByDescending(t => t.Id)
            };

            result.Cars = await cars
                .Skip((currentPage - 1) * carsPerPage)
                .Take(carsPerPage)
                .Select(t => new CarViewModel()//?
                {
                    Id = t.Id,
                    ModelOfCar = t.ModelOfCar,
                    Brand = t.Brand,
                    Price = t.Price,
                    Mileage = t.Mileage,
                    Year = t.Year,
                    ImageUrl = t.ImageUrl,
                    Description = t.Description,
                    Category = t.Category.Name
                })
                .ToListAsync();

            result.TotalCarsCount = await cars.CountAsync();

            return result;
        }
       
        public async Task<IEnumerable<string>> AllCategoriesNames()
        {
            return await repo.AllReadonly<CarCategory>()
                 .Select(c => c.Name)
                 .Distinct()
                 .ToListAsync();
        }
      
        public async Task<IEnumerable<CarViewModel>> GetAllCarsAsync()
        {
            var cars = await repo.AllReadonly<Car>()
            .Where(t => t.IsActive == true)
            .Include(x => x.Owner)
            .Include(c => c.Category)
            .OrderByDescending(t => t.Id)
            .ToListAsync();

            if (cars == null)
            {
                throw new Exception("Car entity error");
            }

            return cars.Select(x => new CarViewModel()
            {
                Id = x.Id,
                Brand = x.Brand,
                ModelOfCar = x.ModelOfCar,
                Description = x.Description,
                Price = x.Price,
                OwnerId = x.Owner.Id,
                OwnerName = x.Owner.UserName,
                Mileage = x.Mileage,
                Year = x.Year,
                Category = x.Category.Name,
                ImageUrl = x.ImageUrl
            });
        }
    
        public async Task<IEnumerable<CarServiceViewModel>> GetLastThreeCars()
        {
            var result = await repo.AllReadonly<Car>()
                           .Where(x => x.IsActive)
                           .OrderByDescending(x => x.Id)
                           .Select(x => new CarServiceViewModel()
                           {
                               Id = x.Id,
                               ImageUrl = x.ImageUrl,
                               Brand = x.Brand,
                               ModelOfCar = x.ModelOfCar,
                               Mileage = x.Mileage,
                               Year = x.Year,
                               Price = x.Price,
                           })
                           .Take(3)
                           .ToListAsync();

            if (result == null)
            {
                throw new Exception("Car entity error");
            }

            return result;
        }
    }
}
