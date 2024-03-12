using AutoRepairService.Infrastructure.Data.EntityModels;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace AutoRepairService.Core.ViewModels
{
    public class RepairModel
    {
        [Required]
        [StringLength(50)]
        public string Brand { get; set; } = null!;

        [Required]
        [StringLength(50)]
        public string CarModel { get; set; } = null!;

        [Display(Name = "Category")]
        public int CategoryId { get; set; }//here need to be 2

        public IEnumerable<CategoryViewModel> RepairCategories { get; set; } = new List<CategoryViewModel>();

        [Required]
        [StringLength(500)]
        public string Description { get; set; } = null!;

        public User? Owner { get; set; } //= null!;

        public string? OwnerName { get; set; } //= null!;

    }
}
