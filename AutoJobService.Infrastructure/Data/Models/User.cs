using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace AutoJobService.Infrastructure.Data.Models
{
    public class User : IdentityUser
    {
        //Rename
        [Required]
        public bool IsMechanic { get; set; }
    }
}
