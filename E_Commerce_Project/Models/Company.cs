using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;

namespace E_Commerce_Project.Models
{
    public class Company
    {
        public int Id { get; set; }
        [Required]
        [MinLength(3, ErrorMessage = "The field must be at least 3 characters."),]
        [MaxLength(100)]
        public string Name { get; set; }

        [Required]
        [MinLength(3, ErrorMessage = "The field must be at least 3 characters."),]
        [MaxLength(100)]
        public string Description { get; set; }

        public int scale { get; set; }
        [ValidateNever]
        public List<Product> products { get; set; }


    }
}
