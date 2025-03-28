using E_Commerce_Project.DataAcess;
using E_Commerce_Project.Models;
using E_Commerce_Project.Repository.Irepositories;

namespace E_Commerce_Project.Repository
{
    public class CompanyRepository : repositories<Company>, ICompanyRepository { 
    ApplicationDbContext dbcontext;
        public CompanyRepository(ApplicationDbContext dbContext) :base(dbContext)
        {
            this.dbcontext = dbContext;
        }
    }
}
