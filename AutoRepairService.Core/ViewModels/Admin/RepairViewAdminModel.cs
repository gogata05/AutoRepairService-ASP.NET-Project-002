﻿using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using AutoRepairService.Infrastructure.Data.EntityModels;

namespace AutoRepairService.Core.ViewModels.Admin
{
    public class RepairViewAdminModel
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
        [StringLength(50)]
        public string Category { get; set; } = null!;

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
        public DateTime StartDate { get; set; }

        public DateTime? EndDate { get; set; }

        public IEnumerable<RepairOffer> RepairsOffers { get; set; } = new List<RepairOffer>();
    }
}
