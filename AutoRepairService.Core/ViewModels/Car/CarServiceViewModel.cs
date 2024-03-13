using System.ComponentModel.DataAnnotations;

namespace AutoRepairService.Core.ViewModels.Car
{
    public class CarServiceViewModel
    {
        public int Id { get; set; }

        public string Brand { get; set; } = null!;

        [Display(Name = "Model")]
        public string ModelOfCar { get; set; } = null!;

        public int Mileage { get; set; }

        public int Year { get; set; }

        [Display(Name = "Image URL")]
        public string ImageUrl { get; set; } = null!;

        public decimal Price { get; set; }

    }
}
