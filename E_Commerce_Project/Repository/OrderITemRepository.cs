using E_Commerce_Project.DataAcess;
using E_Commerce_Project.Models;
using E_Commerce_Project.Repository.Irepositories;

namespace E_Commerce_Project.Repository
{
    public class OrderITemRepository : repositories<OrderItem>, IOrderItemRepository { 
    private readonly ApplicationDbContext _dbcontext;

        public OrderITemRepository(ApplicationDbContext dbContext) : base(dbContext) { 
        this._dbcontext = dbContext;
        }

        public void CreateRange(IEnumerable<OrderItem> orderItems)
        {
            _dbcontext.AddRange(orderItems);
        }
    }

    }
    
    

