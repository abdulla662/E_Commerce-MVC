using E_Commerce_Project.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using E_Commerce_Project.Models.ViewModel;

namespace E_Commerce_Project.DataAcess
{
    public class ApplicationDbContext:IdentityDbContext<ApplicationUser>
    {
        public DbSet<Product> products { get; set; }
        public DbSet<Category> categories { get; set; }

        public DbSet<Company> companies { get; set; }
        public DbSet<Cart> Carts { get; set; }
        public DbSet<Order> orders { get; set; }

        public DbSet<OrderItem> orderItems { get; set; }


        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
    : base(options)
        {
        }
        public ApplicationDbContext()
        {
            
        }

        //legacy code
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
            optionsBuilder.UseSqlServer("Data Source=.;Initial Catalog=E-Commerce;Integrated Security=true;TrustServerCertificate=true");
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Product>().Property(p => p.Price).HasPrecision(18,5);

        }
     
    }
}
