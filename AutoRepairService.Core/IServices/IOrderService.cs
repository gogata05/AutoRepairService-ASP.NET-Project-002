using AutoRepairService.Core.ViewModels.Cart;

namespace AutoRepairService.Core.IServices
{
    public interface IOrderService
    {
        Task<IEnumerable<OrderServiceViewModel>> AllOrdersAsync();

        Task DispatchAsync(int id);
    }
}
