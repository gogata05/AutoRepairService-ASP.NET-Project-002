using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace AutoJobService.Data.Models
{
    public class User : IdentityUser
    {
        //Rename
        [Required]
        public bool IsMechanic { get; set; }
    }
}
