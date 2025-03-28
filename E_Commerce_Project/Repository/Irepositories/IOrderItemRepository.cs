using E_Commerce_Project.Models;

namespace E_Commerce_Project.Repository.Irepositories
{
    public interface IOrderItemRepository : IRepository<OrderItem>
    {
        public void CreateRange (IEnumerable<OrderItem> orderItems);


    }
}
