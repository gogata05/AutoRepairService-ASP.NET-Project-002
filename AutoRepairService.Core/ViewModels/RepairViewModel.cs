using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoRepairService.Core.ViewModels
{
    public class RepairViewModel
    {
        public int Id { get; set; }

        public string Brand { get; set; } = null!;

        public string CarModel { get; set; } = null!;

        public string Category { get; set; } = null!;

        public string Description { get; set; } = null!;//Address

        public string OwnerName { get; set; } = null!;

        public string OwnerId { get; set; }

        public DateTime StartDate { get; set; }
    }
}
