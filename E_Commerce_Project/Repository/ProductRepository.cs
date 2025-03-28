using E_Commerce_Project.DataAcess;
using E_Commerce_Project.Models;
using E_Commerce_Project.Repository.Irepositories;

namespace E_Commerce_Project.Repository
{
    public class ProductRepository : repositories<Product>, IProductRepository
    {
        private readonly ApplicationDbContext dbcontext;
        public ProductRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
            this.dbcontext = dbContext;
        }
    }
}
