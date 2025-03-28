using E_Commerce_Project.DataAcess;
using E_Commerce_Project.Models;
using E_Commerce_Project.Models.ViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Stripe;

namespace E_Commerce_Project.Areas.Customer.Controllers
{
    [Area("Customer")]
    [Authorize]
    public class AccountController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        public AccountController(UserManager<ApplicationUser> userManager)
        {
            this._userManager = userManager;
        }
        public async Task<IActionResult> UserPage()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return RedirectToAction("Login");
            }

            var model = new PersonalAccountVM
            {
                Username = user.UserName,
                Email = user.Email,
                Address = user.Adress,
                ProfilePicture = user.ProfilePicture,
            };

            return View(model);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> UserPage(PersonalAccountVM model, IFormFile profileImage)
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return RedirectToAction("Login");
            }
            if (ModelState.IsValid)
            {
                user.UserName = model.Username;
                user.Email = model.Email;
                user.Adress = model.Address;
                user.ProfilePicture = model.ProfilePicture;
                if (profileImage != null && profileImage.Length > 0)
                {
                    var fileName = Path.GetFileName(profileImage.FileName);
                    var filePath = Path.Combine("wwwroot/images/profiles", fileName);

                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        await profileImage.CopyToAsync(stream);
                    }

                    user.ProfilePicture = "/images/profiles/" + fileName;
                }
                var result = await _userManager.UpdateAsync(user);
                if (result.Succeeded)
                {
                    return RedirectToAction(nameof(UserPage));
                }
            }

            return View(model);
        }
    }
}