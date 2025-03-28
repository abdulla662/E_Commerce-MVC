using System.ComponentModel.DataAnnotations;

namespace E_Commerce_Project.Models.ViewModel
{
    public class PersonalAccountVM
    {
        public string Username { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }
        public string? ProfilePicture { get; set; }
    }
}
