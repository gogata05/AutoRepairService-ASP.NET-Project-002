using AutoRepairService.Core.ViewModels.Car;
using AutoRepairService.Core.ViewModels.Cart;
using AutoRepairService.Infrastructure.Data.EntityModels;
using Microsoft.AspNetCore.Http;

namespace AutoRepairService.Core.IServices
{
    public interface ICartService
    {
        Task<IEnumerable<CarViewModel>> ViewCart(string userId);

        Task<IEnumerable<OrderViewModel>> MyOrder(string userId);

        Task AddToCart(int carId, string userId);

        Task RemoveFromCart(int carId, string userId);

        Task CheckoutCart(IFormCollection collection, string clientId);

        Task<Cart> CartExists(string userId);
    }
}
