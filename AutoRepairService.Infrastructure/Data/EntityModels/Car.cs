using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AutoRepairService.Infrastructure.Data.EntityModels
{
    public class Car
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(50)]
        public string Brand { get; set; } = null!;

        [Required]
        [StringLength(50)]
        public string ModelOfCar { get; set; } = null!;

        [Required]
        public int Mileage { get; set; }

        [Required]
        public int Year { get; set; }

        [Required]
        public int CarCategoryId { get; set; }

        [ForeignKey(nameof(CarCategoryId))]
        public CarCategory Category { get; set; } = null!;

        [Required]
        [StringLength(500)]
        public string Description { get; set; } = null!;

        [Required]
        public string OwnerId { get; set; } = null!;

        [ForeignKey(nameof(OwnerId))]
        public User Owner { get; set; } = null!;

        public bool IsActive { get; set; } = true;

        [Required]
        [Precision(18, 2)]
        public decimal Price { get; set; }

        [StringLength(500)]
        public string ImageUrl { get; set; } = null!;

        public IEnumerable<CarCart> CarsCarts { get; set; } = new List<CarCart>();
    }
}
