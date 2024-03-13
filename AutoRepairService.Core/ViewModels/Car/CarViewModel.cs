namespace AutoRepairService.Core.ViewModels.Car
{
    public class CarViewModel : CarServiceViewModel
    {

        public int OrderQuantity { get; set; } = 1;

        public string? Category { get; set; }

        public string Description { get; set; } = null!;

        public string? OwnerName { get; set; }

        public string OwnerId { get; set; } = null!;

        public decimal TotalPrice { get; set; }

    }
}
