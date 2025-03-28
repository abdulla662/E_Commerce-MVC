using Microsoft.AspNetCore.Mvc;

namespace E_Commerce_Project.Areas.ErrorPage.Controllers
{
    [Area("ErrorPage")]
    public class ErrorController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Errormodel()
        {
            return View();
        }
    }
}
