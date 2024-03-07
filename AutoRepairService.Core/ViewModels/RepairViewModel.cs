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

        public string Model { get; set; } = null!;

        //public int Mileage { get; set; }

        //public int Year { get; set; }

        public string Category { get; set; } = null!;

        public string Description { get; set; } = null!;

        public string OwnerName { get; set; } = null!;

        public string OwnerId { get; set; } = null!;

        public DateTime StartDate { get; set; }
    }
}
