using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace E_Commerce_Project.Models
{
    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; }
        [Required]
        [MinLength(3)]
        [MaxLength(100)]
        public string? Description { get; set; }


        [Precision(18, 2)] 
        public decimal Price { get; set; }
        [Range(0, 100000)]

        [RegularExpression("/^.*\\.(png|jpg)$/i\r\n",ErrorMessage =("الصوره لازم تبقي png "))]
        public string? Img {  get; set; }
        public double?  Rate { get; set; }
        [Range(0, 400)]

        public int Quantity { get; set; }
        public int CategoryId { get; set; }

        public int? CompanyId { get; set; }

        public decimal? Discount { get; set; }
        [ValidateNever]
        public Category Category { get; set; }


        [ValidateNever]
        public Company Company { get; set; }



    }
}
