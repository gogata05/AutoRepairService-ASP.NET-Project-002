using AutoRepairService.Core.IServices;
using AutoRepairService.Core.ViewModels;
using AutoRepairService.Core.ViewModels.Car;
using AutoRepairService.Infrastructure.Data.Common;
using AutoRepairService.Infrastructure.Data.EntityModels;
using Microsoft.EntityFrameworkCore;

namespace AutoRepairService.Core.Services
{
    public class AdminCarService : IAdminCarService
    {
        private const string DefaultImageUrl = "https://media.istockphoto.com/id/1178775481/vector/service-cars-icon-isolated-on-white-background-vector-illustration.jpg?s=612x612&w=0&k=20&c=VoGBYuv5vEW_Zbt2KIqcj2-sfEp21FGUlbZaq6QRfYY=";

        private readonly IRepository repo;

        public AdminCarService(IRepository _repo)
        {
            repo = _repo;
        }

        public async Task AddCarAsync(CarModel model, string id)
        {
            var user = await repo.GetByIdAsync<User>(id);

            if (user == null)
            {
                throw new Exception("User entity error");
            }

            var car = new Car()
            {
                Brand = model.Brand,
                ModelOfCar = model.ModelOfCar,
                Mileage = model.Mileage,
                Year = model.Year,
                CarCategoryId = model.CategoryId,
                Description = model.Description,
                Owner = user,
                OwnerId = user.Id,
                Price = model.Price,
                IsActive = true,
                ImageUrl = model.ImageUrl != null ? model.ImageUrl : DefaultImageUrl
            };


            await repo.AddAsync(car);
            await repo.SaveChangesAsync();

        }
 
        public async Task<IEnumerable<CategoryViewModel>> AllCategories()
        {
            return await repo.AllReadonly<CarCategory>()
                .Select(x => new CategoryViewModel()
                {
                    Id = x.Id,
                    Name = x.Name
                })
                .ToListAsync();
        }

        public async Task<bool> CategoryExists(int categoryId) // check if needed
        {
            return await repo.AllReadonly<CarCategory>()
                          .AnyAsync(c => c.Id == categoryId);
        }
      
        public async Task<bool> CarExistAsync(int carId) // check if needed
        {
            return await repo.AllReadonly<Car>()
                          .AnyAsync(c => c.Id == carId);
        }
       
        public async Task<CarModel> GetEditAsync(int id, string userId)
        {
            if (!await CarExistAsync(id))
            {
                throw new Exception("Car don't exist!");
            }

            var car = await repo.All<Car>().Where(x => x.Id == id).Include(x => x.Owner).FirstOrDefaultAsync();

            if (car.Owner.Id != userId)
            {
                throw new Exception("User is not owner");
            }

            if (car.IsActive == false)
            {
                throw new Exception("This car is not active");
            }

            var model = new CarModel()
            {
                Brand = car.Brand,
                ModelOfCar = car.ModelOfCar,
                Mileage = car.Mileage,
                Year = car.Year,
                CategoryId = car.CarCategoryId,
                Description = car.Description,
                Price = car.Price,
                ImageUrl = car.ImageUrl

            };
            return model;
        }
     
        public async Task PostEditAsync(int id, CarModel model)
        {
            if (!await CarExistAsync(id))
            {
                throw new Exception("Car don't exist!");
            }

            var car = await repo.All<Car>().Where(x => x.Id == id).FirstOrDefaultAsync();

            car.Brand = model.Brand;
            car.ModelOfCar = model.ModelOfCar;
            car.CarCategoryId = model.CategoryId;
            car.Description = model.Description;
            car.CarCategoryId = model.CategoryId;
            car.Mileage = model.Mileage;
            car.Year = model.Year;
            car.ImageUrl = model.ImageUrl;
            car.Price = model.Price;

            await repo.SaveChangesAsync();
        }
        
        public async Task RemoveCarAsync(int id, string userId)
        {
            var car = await repo.GetByIdAsync<Car>(id);

            if (car == null)
            {
                throw new Exception("Invalid car Id");
            }

            if (car.OwnerId != userId)
            {
                throw new Exception("Invalid user Id");
            }

            car.IsActive = false;
            await repo.SaveChangesAsync();
        }
    }
}
