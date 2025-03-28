using E_Commerce_Project.DataAcess;
using E_Commerce_Project.Models;
using E_Commerce_Project.Repository;
using E_Commerce_Project.Repository.Irepositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace E_Commerce_Project.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class CategoryController : Controller
    {
        private readonly ICategoryRepository categoryRepository;
        public CategoryController(ICategoryRepository categoryRepository)
        {
            this.categoryRepository = categoryRepository;
        }

        public IActionResult Index()
        {
            var categories = categoryRepository.Get();

            return View(categories.ToList());
        }
        [HttpGet]
        public IActionResult Edit(int CategoryId)
        {
            var category = categoryRepository.GetOne(e=> e.Id == CategoryId);
            if (category != null)
            {
                return View(category);

            }
            return RedirectToAction("Error", "Home");
        }
        [HttpPost]
        public IActionResult Edit(Category category)
        {
            if (ModelState.IsValid) { 
                categoryRepository.Edit(category);
                categoryRepository.comit();
                TempData["Notification"] = "Updated  Succefully";

                return RedirectToAction(nameof(Index));
            }
            return View(category);
            

        }

        [HttpGet]
        public IActionResult create()
        {
            return View(new Category());
        }
        [HttpPost]

        public IActionResult create(Category category)
        {
            //ModelState.Remove("products");
            if (ModelState.IsValid) { 
           categoryRepository.Create(category);
                categoryRepository.comit();
                TempData["Notification"] = "Category Created Succefully";
                return RedirectToAction(nameof( Index));

            }
            return View(category);

        }
        public IActionResult Delete(int CategoryId) {
            var category = categoryRepository.GetOne(e=> e.Id==CategoryId);
            if (category != null) {
                categoryRepository.Delete(category);
                categoryRepository.comit();
                TempData["Notification"] = "Delete Category Succefully";



            }
            return RedirectToAction(nameof(Index));
        }
       
    }
}
