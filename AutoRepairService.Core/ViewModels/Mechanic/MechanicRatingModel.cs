using System.ComponentModel.DataAnnotations;

namespace AutoRepairService.Core.ViewModels.Mechanic
{
    public class MechanicRatingModel
    {
        [Required]
        public string MechanicId { get; set; } = null!;

        [Required]
        public string UserId { get; set; } = null!;

        [Required]
        public int RepairId { get; set; }

        [StringLength(200)]
        public string? Comment { get; set; }

        [Required]
        [Range(1, 5)]
        [Display(Name = "selected stars")]
        public int Points { get; set; }
    }
}
