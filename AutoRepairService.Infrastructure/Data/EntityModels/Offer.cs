using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace AutoRepairService.Infrastructure.Data.EntityModels
{
    public class Offer
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(500)]
        public string Description { get; set; } = null!;

        [ForeignKey(nameof(OwnerId))]
        public User Owner { get; set; } = null!;

        [Required]
        public string OwnerId { get; set; } = null!;

        [Required]
        [Precision(18, 2)]
        public decimal Price { get; set; }

        public bool? IsAccepted { get; set; } = null;

        public IEnumerable<RepairOffer> RepairsOffers { get; set; } = new List<RepairOffer>();

        [Required]
        public bool IsActive { get; set; } = true;
    }
}
