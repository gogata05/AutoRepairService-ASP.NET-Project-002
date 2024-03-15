using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace AutoRepairService.Core.ViewModels.Offer
{
    public class OfferViewModel
    {
        [Required]
        public string Description { get; set; } = null!;

        [Required]
        public string OwnerId { get; set; } = null!;

        //[Required]
        public string? FirstName { get; set; }

        //[Required]
        public string? LastName { get; set; }

        [Required]
        public int RepairId { get; set; }

        [Required]
        public decimal Price { get; set; }
    }
}
