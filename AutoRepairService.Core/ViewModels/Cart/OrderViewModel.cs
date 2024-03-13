using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoRepairService.Core.ViewModels.Cart
{
    public class OrderViewModel
    {
        public int OrderNumber { get; set; }

        public string Status { get; set; } = null!;

        public string OrderAdress { get; set; } = null!;

        public bool IsCompleted { get; set; }

        public DateTime ReceivedOn { get; set; }

        public DateTime? CompletedOn { get; set; }
    }
}
