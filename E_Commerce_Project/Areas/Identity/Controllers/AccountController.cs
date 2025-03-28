using E_Commerce_Project.Models;
using E_Commerce_Project.Models.ViewModel;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace E_Commerce_Project.Areas.Identity.Controllers
{
    [Area("Identity")]
    public class AccountController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly RoleManager<IdentityRole> _roleManger;
        public AccountController(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, RoleManager<IdentityRole> ruleManager)
        {
            this._userManager = userManager;
            this._signInManager = signInManager;
            this._roleManger = ruleManager;
        }
        [HttpGet]
        public async Task<IActionResult> Register()
        {
            if (_roleManger.Roles.IsNullOrEmpty())
            {
                await _roleManger.CreateAsync(new IdentityRole("Super Admin"));
                await _roleManger.CreateAsync(new IdentityRole("Admin"));
                await _roleManger.CreateAsync(new IdentityRole("Customer"));
                await _roleManger.CreateAsync(new IdentityRole("User"));

            }
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(RegisterVM registerVM)
        {
            if (ModelState.IsValid)
            {
                ApplicationUser applicationUser = new ApplicationUser()
                {
                    UserName = registerVM.Username,
                    Email = registerVM.email,
                    Adress = registerVM.Adress,
                };
                var result = await _userManager.CreateAsync(applicationUser, registerVM.password);
                if (result.Succeeded)
                {
                    await _signInManager.SignInAsync(applicationUser, false);

                    await _userManager.AddToRoleAsync(applicationUser, "Customer");
                    return RedirectToAction("index", "Home", new { area = "Customer" });
                }
                else
                    ModelState.AddModelError("Password", "don't match the constraints");

            }
            return View(registerVM);

        }
        [HttpGet]
        public IActionResult login()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> login(LoginVM loginVM)
        {
            if (ModelState.IsValid)
            {
                var appuser = await _userManager.FindByEmailAsync(loginVM.Email);
                if (appuser != null)
                {
                    var result = await _userManager.CheckPasswordAsync(appuser, loginVM.Password);

                    if (await _userManager.IsLockedOutAsync(appuser))
                    {
                        TempData["Error"] = "Your account is locked. Please try again later!";
                        return RedirectToAction("Login");
                    }
                    if (result == true)
                    {
                        await _signInManager.SignInAsync(appuser, loginVM.RememberMe);



                        return RedirectToAction("Index", "Home", new { area = "Customer" });
                    }
                    else
                    {
                        ModelState.AddModelError("Password", "Sorry Password is not correct");
                    }
                }
                else
                {
                    ModelState.AddModelError("Email", "Sorry Email is not correct");
                }
            }
            return View(loginVM);

        }
        public IActionResult logout()
        {
            _signInManager.SignOutAsync();
            return RedirectToAction("Login", "Account", new { area = "Identity" });
        }
        public IActionResult AccessDenied() => View();

    }
}
