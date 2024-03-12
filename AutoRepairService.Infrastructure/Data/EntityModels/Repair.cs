using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace AutoRepairService.Infrastructure.Data.EntityModels
{
    public class Repair
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(50)]
        public string Brand { get; set; } = null!;

        [Required]
        [StringLength(50)]
        public string CarModel { get; set; } = null!;

        [Required]
        public int RepairCategoryId { get; set; }

        [ForeignKey(nameof(RepairCategoryId))]
        public RepairCategory Category { get; set; } = null!;

        [Required]
        [StringLength(500)]
        public string Description { get; set; } = null!;//Address

        [Required]
        public string OwnerId { get; set; } = null!;

        [ForeignKey(nameof(OwnerId))]
        public User Owner { get; set; } = null!;

        [StringLength(50)]
        public string? OwnerName { get; set; }

        public string? MechanicId { get; set; }

        [Required]
        public bool IsTaken { get; set; }

        [Required]
        public bool IsActive { get; set; } = false;
        //[Required]
        public bool IsApproved { get; set; } = false;

        [Required]
        public string Status { get; set; } = "Pending";

        [Required]
        public DateTime StartDate { get; set; }

        public DateTime? EndDate { get; set; }

        public IEnumerable<RepairOffer> RepairsOffers { get; set; } = new List<RepairOffer>();

        [Required]
        public int RepairStatusId { get; set; } = 1;

        [ForeignKey(nameof(RepairStatusId))]
        public RepairStatus RepairStatus { get; set; } = null!;

    }
}
