using E_Commerce_Project.DataAcess;
using E_Commerce_Project.Models;
using E_Commerce_Project.Repository.Irepositories;

namespace E_Commerce_Project.Repository
{
    public class ApplicationUserRepository : repositories<ApplicationUser>, IApplicationUserReposatory
    {
        public ApplicationUserRepository(ApplicationDbContext applicationDb) : base(applicationDb)
        {
        }
    }
}