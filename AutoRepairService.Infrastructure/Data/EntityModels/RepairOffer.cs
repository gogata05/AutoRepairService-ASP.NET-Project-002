using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AutoRepairService.Infrastructure.Data.EntityModels
{
    public class RepairOffer
    {
        [Required]
        public int RepairId { get; set; }

        [ForeignKey(nameof(RepairId))]
        public Repair Repair { get; set; } = null!;

        [Required]
        public int OfferId { get; set; }

        [ForeignKey(nameof(OfferId))]
        public Offer Offer { get; set; } = null!;
    }
}
