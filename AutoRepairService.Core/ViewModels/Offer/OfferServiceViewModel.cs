namespace AutoRepairService.Core.ViewModels.Offer
{
    public class OfferServiceViewModel : OfferViewModel
    {
        public int Id { get; set; }

        public string Rating { get; set; } = null!;

        public string MechanicName { get; set; } = null!;

        public string MechanicPhoneNumber { get; set; } = null!;

        public string RepairBrand { get; set; } = null!;

        public string RepairModel { get; set; } = null!;

        public string RepairDescription { get; set; } = null!;

        public string RepairCategory { get; set; } = null!;

        public bool? IsAccepted { get; set; }
    }
}
