
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using AutoRepairService.Infrastructure.Data.EntityModels;

namespace AutoRepairService.Areas.Admin.Models
{
    public class OfferViewAdminModel
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(500)]
        public string Description { get; set; } = null!;

        [ForeignKey(nameof(OwnerId))]
        public List<User> Owner { get; set; } = new List<User>();

        [Required]
        public string OwnerId { get; set; }  //null!; 
                                             // or int and add mechanic entity
        [Required]
        public decimal Price { get; set; }
        //time

        public bool? IsAccepted { get; set; } = null;

        public IEnumerable<RepairOffer> RepairsOffers { get; set; } = new List<RepairOffer>();
    }
}
