using System.ComponentModel.DataAnnotations;

namespace E_Commerce_Project.Models.ViewModel
{
    public class LoginVM
    {
        public int id { get; set; }

        [Required]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }


        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        public bool RememberMe { get; set; }
    }
}
