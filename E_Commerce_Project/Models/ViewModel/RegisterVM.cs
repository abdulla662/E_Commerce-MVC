using System.ComponentModel.DataAnnotations;

namespace E_Commerce_Project.Models.ViewModel
{
    public class RegisterVM
    {
        public int id { get; set; }
        [Required]
        public string Username { get; set; }
        [Required]
        [DataType(DataType.EmailAddress)]
        public string email { get; set; }
        [Required]
        [DataType(DataType.Password)]
        public string password { get; set; }
        [Required]
        [DataType(DataType.Password)]
        [Compare(nameof(password))]
        public string ConfirmPassword { get; set; }

        public string Adress { get; set; }
    }
}
