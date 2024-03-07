namespace AutoRepairService.Core.ViewModels.Offer
{
    public class MyOffersViewModel
    {
        public int OfferId { get; set; }

        public string Description { get; set; } = null!;

        public string MechanicName { get; set; } = null!;

        public decimal Price { get; set; }
        
        public string RepairOwnerId { get; set; } = null!;

        public string RepairOwnerName { get; set; } = null!;

        public bool? IsAccepted { get; set; }
    }
}
