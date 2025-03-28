using E_Commerce_Project.DataAcess;
using E_Commerce_Project.Models;
using E_Commerce_Project.Repository.Irepositories;

namespace E_Commerce_Project.Repository
{
    public class cartRepository : repositories<Cart>, ICartRepository
    {
        private readonly ApplicationDbContext dbcontext;
        public cartRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
            this.dbcontext = dbContext;
        }
    }
}
