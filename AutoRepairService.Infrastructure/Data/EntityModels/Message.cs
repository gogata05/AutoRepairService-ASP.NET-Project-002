using System.ComponentModel.DataAnnotations;

namespace AutoRepairService.Infrastructure.Data.EntityModels
{
    public class Message
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string UserId { get; set; } = null!;

        [Required]
        public string ConnectionId { get; set; } = null!;

        [Required]
        public DateTime CreatedOn { get; set; }

        [Required]
        public bool IsActive { get; set; } = true;
    }
}
