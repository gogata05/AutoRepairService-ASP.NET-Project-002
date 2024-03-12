using System.ComponentModel.DataAnnotations;

namespace AutoRepairService.Infrastructure.Data.EntityModels
{
    public class RepairStatus
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        public IEnumerable<Repair> Repairs { get; set; } = new List<Repair>();
    }
}
