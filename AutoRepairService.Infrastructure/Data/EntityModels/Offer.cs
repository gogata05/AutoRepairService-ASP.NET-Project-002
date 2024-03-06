using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoRepairService.Infrastructure.Data.EntityModels
{
    public class Offer
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(500)]
        public string Description { get; set; } = null!;

        //[Required]
        //public decimal Price { get; set; }

        [ForeignKey(nameof(OwnerId))]
        public User Owner { get; set; } = null!;

        [Required]
        public string OwnerId { get; set; } = null!;

        [Required]
        [ForeignKey(nameof(Repair))]
        public int RepairId { get; set; }

        [Required]
        public Repair Repair { get; set; } = null!;
    }
}
