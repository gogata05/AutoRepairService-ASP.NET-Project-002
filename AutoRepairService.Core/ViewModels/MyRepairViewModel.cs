using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoRepairService.Core.ViewModels
{
    public class MyRepairViewModel
    {
        public int Id { get; set; }

        public string OwnerId { get; set; } = null!;

        public string Brand { get; set; } = null!;

        public string Model { get; set; } = null!;

        public string Category { get; set; } = null!;

        public string Description { get; set; } = null!;

        public bool IsTaken { get; set; }

        public bool IsActive { get; set; } = false;

        public bool IsApproved { get; set; } = false;

        public string Status { get; set; } = null!;

        public string? MechanicId { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime? EndDate { get; set; }
    }
}
