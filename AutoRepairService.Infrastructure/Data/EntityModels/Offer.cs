﻿using System;
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

        [ForeignKey(nameof(OwnerId))]
        public List<User> Owner { get; set; } = new List<User>();

        [Required]
        public string OwnerId { get; set; } = null!;
        // or int and add Mechanic entity
        [Required]
        public decimal Price { get; set; }
        //time

        public bool? IsAccepted { get; set; } = null;

        public IEnumerable<RepairOffer> RepairsOffers { get; set; } = new List<RepairOffer>();
    }
}
