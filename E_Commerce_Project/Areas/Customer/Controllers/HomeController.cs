using E_Commerce_Project.DataAcess;
using E_Commerce_Project.Models;
using E_Commerce_Project.Views.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
namespace E_Commerce_Project.Controllers
{
    [Area("Customer")]

    public class HomeController : Controller
    {
        ApplicationDbContext dbContext=new ApplicationDbContext();
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index(string? CategoryName,int? Rating,string? Pname,double? minrange,double? maxrange)
        {
            IQueryable<Product> data = dbContext.products.Include(e => e.Category);

            if (CategoryName != null) {
                data=data.Where(e => e.Category.Name == CategoryName);
            }
            if (Pname != null)
            {
               data= data.Where(e => e.Name.Contains(Pname));
            }
            if (minrange != null)
            {
                data = data.Where(e => e.Price >= (decimal)minrange);
            }

            if (maxrange != null)
            {
                data = data.Where(e => e.Price <= (decimal)maxrange);
            }

            if (Rating != null)
            {
                data = data.Where(e => e.Rate >= Rating);
            }
            if (CategoryName != null)
            {
                data = data.Where(e => e.Category.Name==CategoryName);
            }

            var Categories = dbContext.categories.ToList();
            var count=dbContext.orders.Count();
            ViewBag.OrderCount = count;

            //var CategoriesWithProducts = new
            //{
            //    products= data.ToList(),
            //    categories= Categories.ToList()
            //};
            ViewData["Categories"] = Categories;
            return View(data.ToList());
        }
        public IActionResult Details(int productId)
        {


            var product = dbContext.products.Include(e => e.Category)
                                            .FirstOrDefault(e => e.Id == productId);
            if (product == null)
            {
                return RedirectToAction(nameof(Index)); 
            }


            if (product != null) 
            {
                var relatedproducts = dbContext.products.Include(e => e.Category).Where(e => e.Name == product.Name).Skip(0).Take(4).ToList();

                ProductsWithCategory productsWithCategory = new ProductsWithCategory()
                {
                    product = product,
                    productsWithCategoryName = relatedproducts
                };

                return View(productsWithCategory);
            }

            return RedirectToAction(nameof(NotFoundPage)); 
        }

        public IActionResult Privacy()
        {
            return View();
        }
        public IActionResult NotFoundPage() { 

        return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }


    }
}
