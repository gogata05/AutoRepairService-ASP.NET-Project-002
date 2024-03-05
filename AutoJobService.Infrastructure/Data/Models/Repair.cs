using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
namespace AutoJobService.Infrastructure.Data.Models
{
    public class Repair
    {
        //Rename
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(50)]
        public string Name { get; set; } = null!;

        [Required]
        [StringLength(50)]
        public string Category { get; set; }

        [Required]
        [StringLength(500)]
        public string Description { get; set; } //Address

        [Required]
        public string OwnerId { get; set; }

        [ForeignKey(nameof(OwnerId))]
        public User Owner { get; set; }

        public string? MechanicId { get; set; }

        [Required]
        public bool IsTaken { get; set; }

        [Required]
        public DateTime StartDate { get; set; }

        public DateTime? EndDate { get; set; }

    }
}
