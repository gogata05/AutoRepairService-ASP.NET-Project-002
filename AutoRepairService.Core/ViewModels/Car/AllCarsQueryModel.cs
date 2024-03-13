using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoRepairService.Core.ViewModels.Car
{
    public class AllCarsQueryModel
    {
        public const int CarsPerPage = 3;

        public string? Category { get; set; }

        public string? SearchTerm { get; set; }

        public CarSorting Sorting { get; set; }

        public int CurrentPage { get; set; } = 1;

        public int TotalCarsCount { get; set; }

        public IEnumerable<string> Categories { get; set; } = Enumerable.Empty<string>();

        public IEnumerable<CarViewModel> Cars { get; set; } = Enumerable.Empty<CarViewModel>();
    }
}
