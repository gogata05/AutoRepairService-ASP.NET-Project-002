using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoRepairService.Core.ViewModels.Cart
{
    public class OrderServiceViewModel : OrderViewModel
    {
        public decimal TotalCost { get; set; }

        public string Details { get; set; } = null!;
    }
}
