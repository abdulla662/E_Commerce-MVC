using E_Commerce_Project.Models;

namespace E_Commerce_Project.Views.ViewModels
{
    public class ProductsWithCategory
    {
        public Product product { get; set; }
        public List<Product>  productsWithCategoryName{ get; set; }
    }
}
