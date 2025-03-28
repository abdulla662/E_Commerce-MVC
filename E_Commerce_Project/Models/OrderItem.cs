
namespace E_Commerce_Project.Models
{
    public class OrderItem
    {
        public int Id { get; set; }

        public int OrderId { get; set; }
        public Order order { get; set; }

        public int ProductId { get; set; }

        public Product product { get; set; }

        public int Count { get; set; }

        public double price { get; set; }

    }
}
