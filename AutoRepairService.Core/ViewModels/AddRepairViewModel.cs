using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoRepairService.Core.ViewModels
{
    public class AddRepairViewModel
    {

        [Required]
        [StringLength(50)]
        public string Title { get; set; } = null!;

        [Required]
        [StringLength(50)]
        public string Category { get; set; }

        [Required]
        [StringLength(500)]
        public string Description { get; set; } //Address     
    }
}
