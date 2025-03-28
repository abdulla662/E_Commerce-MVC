using Microsoft.EntityFrameworkCore;

namespace E_Commerce_Project.Models
{
    [PrimaryKey(nameof(ProductId), nameof(ApplicationUserId))]

    public class Cart
    {
        public int ProductId { get; set; }
        public Product Product { get; set; }

        public string ApplicationUserId { get; set; }

        public ApplicationUser ApplicationUser { get; set; }

        public int Count { get; set; }
    }
}
