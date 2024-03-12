using System.ComponentModel.DataAnnotations;

namespace AutoRepairService.Infrastructure.Data.EntityModels
{
    public class RepairCategory
    {

        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(50)]
        public string Name { get; set; } = null!;

        public List<Repair> Repairs { get; set; } = new List<Repair>();
    }
}
