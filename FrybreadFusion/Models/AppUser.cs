using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;


namespace FrybreadFusion.Models
{
    public class AppUser : IdentityUser
    {
        [Required(ErrorMessage = "Full name is required.")]
        [StringLength(50, ErrorMessage = "Full name cannot be longer than 50 characters.")]

        public string? FullName { get; set; }
    }
}
