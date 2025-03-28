using Microsoft.AspNetCore.Identity;

namespace E_Commerce_Project.Models
{
    public class ApplicationUser:IdentityUser
    {
        public string? ProfilePicture { get; set; }

        public string? Adress { get; set; }

    }
}
