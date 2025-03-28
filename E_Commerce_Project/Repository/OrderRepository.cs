using E_Commerce_Project.DataAcess;
using E_Commerce_Project.Models;
using E_Commerce_Project.Repository.Irepositories;


namespace E_Commerce_Project.Repository
{
    public class OrderRepository : repositories<Order>, IOrderRepository {


        private readonly ApplicationDbContext dbContext;
        public OrderRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
           this.dbContext = dbContext;
        }
    }
}
