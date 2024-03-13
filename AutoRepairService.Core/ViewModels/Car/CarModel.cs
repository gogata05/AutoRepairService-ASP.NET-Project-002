using System.ComponentModel.DataAnnotations;

namespace AutoRepairService.Core.ViewModels.Car
{
    public class CarModel
    {
        [Required]
        [StringLength(50, MinimumLength = 3)]
        public string Brand { get; set; } = null!;

        [Required]
        [StringLength(50, MinimumLength = 3)]
        [Display(Name = "Model")]
        public string ModelOfCar { get; set; } = null!;

        [Required]
        public int Mileage { get; set; }

        [Required]
        public int Year { get; set; }


        [Display(Name = "Category")]
        public int CategoryId { get; set; }

        public IEnumerable<CategoryViewModel> CarCategories { get; set; } = new List<CategoryViewModel>();

        [Required]
        [StringLength(500, MinimumLength = 10)]
        public string Description { get; set; } = null!;

        [Url]
        [StringLength(500, MinimumLength = 5)]
        public string? ImageUrl { get; set; }

        [Required]
        [Range(typeof(decimal), "0.01", "10000")]
        public decimal Price { get; set; }
    }
}
