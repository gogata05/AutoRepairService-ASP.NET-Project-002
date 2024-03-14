using System.Text;
using AutoRepairService.Core.IServices;
using AutoRepairService.Core.ViewModels.Car;
using AutoRepairService.Core.ViewModels.Cart;
using AutoRepairService.Infrastructure.Data.Common;
using AutoRepairService.Infrastructure.Data.EntityModels;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

namespace AutoRepairService.Core.Services
{
    public class CartService : ICartService
    {
        private readonly IRepository repo;

        public CartService(IRepository _repo)
        {
            repo = _repo;
        }
      
        public async Task AddToCart(int carId, string userId)
        {
            var cart = await CartExists(userId);
            var car = await repo.AllReadonly<Car>().Where(x => x.Id == carId).FirstOrDefaultAsync();

            if (car == null)
            {
                throw new Exception("Car don't exist");
            }

            bool carCartExist = await repo.AllReadonly<CarCart>()
                .Where(x => x.CartId == cart.Id && x.CarId == car.Id)
                .AnyAsync();
            if (carCartExist)
            {
                throw new Exception("Car already in cart");
            }

            var carcart = new CarCart()
            {
                CartId = cart.Id,
                CarId = car.Id
            };

            await repo.AddAsync(carcart);
            await repo.SaveChangesAsync();
        }
       
        public async Task<Cart> CartExists(string userId)
        {
            Cart userCart;
            var cartExist = await repo.AllReadonly<Cart>().Where(x => x.UserId == userId).AnyAsync();

            if (!cartExist)
            {
                var user = await repo.GetByIdAsync<User>(userId);
                userCart = new Cart()
                {
                    User = user,
                    UserId = user.Id
                };
                await repo.AddAsync(userCart);
                await repo.SaveChangesAsync();
            }
            else
            {
                userCart = await repo.AllReadonly<Cart>().Where(x => x.UserId == userId).FirstAsync();
            }
            return userCart;
        }
     
        public async Task RemoveFromCart(int carId, string userId)
        {
            var cart = await repo.AllReadonly<Cart>()
                .Where(x => x.UserId == userId)
                .FirstOrDefaultAsync();

            if (cart == null)
            {
                throw new Exception("Cart don't exist");
            }

            var carCart = await repo.All<CarCart>()
                .Where(x => x.CartId == cart.Id && x.CarId == carId)
                .FirstOrDefaultAsync();

            if (carCart == null)
            {
                throw new Exception("Car not in the cart");
            }

            repo.Delete<CarCart>(carCart);
            await repo.SaveChangesAsync();
        }
        
        public async Task CheckoutCart(IFormCollection collection, string clientId)
        {
            var count = collection["item.Id"].Count;

            if (count == 0)
            {
                throw new Exception("Invalid data");
            }

            var sb = new StringBuilder();
            sb.AppendLine("{");
            for (int i = 0; i < count; i++)
            {
                sb.AppendLine($"\"item{i + 1}\" : ");
                sb.Append("{");
                sb.AppendLine($"\"itemId\" : \"{collection["item.Id"][i]}\",");
                sb.AppendLine($"\"Ordered\" : \"{collection["item.OrderQuantity"][i]}\",");
                sb.AppendLine($"\"Price\" : \"{collection["item.Price"][i]:F2}\",");
                sb.AppendLine($"\"Cost\" : \"{collection["cost"][i]:F2}\"");
                sb.Append("},");
            }
            sb.Append($"\"TotalCost\":\"{collection["total"]}\",");
            sb.Append($"\"itemsInOrder\":\"{count}\",");
            sb.Append($"\"BuyerId\":\"{clientId}\"");
            sb.Append($"\"Address\":\"{collection["address"]}\"");
            sb.Append($"\"CityId\":\"{collection["City"]}\"");
            sb.Append($"\"OfficeLocation\":\"{collection["Office"]}\"");
            sb.Append("}");


            var itemsDetails = sb.ToString().Trim();

            var order = new Order()
            {
                TotalCost = decimal.Parse($"{collection["total"]:F2}"),
                ClientId = clientId,
                ItemsDetails = itemsDetails,
                ReceivedOn = DateTime.Now,
                Status = "Preparing",
                OrderAdress = collection["address"]
            };

            await repo.AddAsync<Order>(order);

            var carIds = collection["item.Id"];

            foreach (var id in carIds)
            {
                await RemoveFromCart(int.Parse(id), clientId);
            };

            await repo.SaveChangesAsync();
        }
       
        public async Task<IEnumerable<CarViewModel>> ViewCart(string userId)
        {
            var cart = await CartExists(userId);

            var cars = await repo.AllReadonly<CarCart>().Where(c => c.CartId == cart.Id).Include(t => t.Car).Include(c => c.Car.Category).Include(t => t.Car.Owner).Select(x => new CarViewModel()
            {
                Id = x.Car.Id,
                Brand = x.Car.Brand,
                ModelOfCar = x.Car.ModelOfCar,
                Mileage = x.Car.Mileage,
                Year = x.Car.Year,
                Price = x.Car.Price,
                Category = x.Car.Category.Name,
                Description = x.Car.Description,
                OwnerId = x.Car.Owner.Id,
                OwnerName = x.Car.Owner.UserName
            }).ToListAsync();

            if (cars == null)
            {
                throw new Exception("Cars DB error");
            }

            return cars;

        }
       
        public async Task<IEnumerable<OrderViewModel>> MyOrder(string userId)
        {
            var orders = await repo.AllReadonly<Order>()
                .Where(c => c.ClientId == userId)
                .Select(x => new OrderViewModel
                {
                    OrderNumber = x.Id,
                    OrderAdress = x.OrderAdress,
                    ReceivedOn = x.ReceivedOn,
                    Status = x.Status,
                    CompletedOn = x.CompletedOn,
                    IsCompleted = x.IsCompleted
                })
                .ToListAsync();

            return orders;
        }
    }
}
