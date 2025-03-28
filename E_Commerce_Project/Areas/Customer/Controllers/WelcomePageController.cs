using Microsoft.AspNetCore.Mvc;

namespace E_Commerce_Project.Controllers
{
    [Area("Customer")]


    public class WelcomePageController : Controller
    {
        
        public IActionResult Home()
        {
    
            return View();
        }
    }
}
