using System.ComponentModel.DataAnnotations;

namespace AutoRepairService.Infrastructure.Data.EntityModels
{
    public class Rating
    {
        [Key]
        public int Id { get; set; }

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
        public int Points { get; set; }

    }
}