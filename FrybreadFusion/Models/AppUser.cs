using Microsoft.AspNetCore.Identity;

namespace FrybreadFusion.Models
{
    public class AppUser : IdentityUser
    {
 
        public string? FullName { get; set; }
    }
}
