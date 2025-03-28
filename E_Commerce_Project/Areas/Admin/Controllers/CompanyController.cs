using E_Commerce_Project.DataAcess;
using E_Commerce_Project.Models;
using E_Commerce_Project.Repository;
using E_Commerce_Project.Repository.Irepositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.Design;

namespace E_Commerce_Project.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]

    public class CompanyController : Controller
    {
        //ApplicationDbContext dbContext = new ApplicationDbContext();
        //CompanyRepository CompanyRepository =new CompanyRepository();
        private readonly ICompanyRepository companyRepository;
        public CompanyController(ICompanyRepository companyRepository) { 
        this.companyRepository = companyRepository;
        }
        public IActionResult Index()
        {
            var companies = companyRepository.Get();
            return View(companies.ToList());
        }
        [HttpGet]
        public IActionResult create()
        {
            var data = companyRepository.Get();
            return View(new Company());
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult create(Company company)
        {
            //ModelState.Remove("products");
            if (ModelState.IsValid)
            {
                companyRepository.Create(company);
                companyRepository.comit();
                return RedirectToAction(nameof(Index));
            }
            return View(company);

        }
        public IActionResult Delete(int companyId)
        {
            var company = companyRepository.GetOne(e=> e.Id == companyId);

            if (company == null)
            {
                return RedirectToAction("error", "Home");

            }

            companyRepository.Delete(company);
            companyRepository.comit();

            return RedirectToAction(nameof(Index));
        }

        public IActionResult Edit(int CompanyId)
        {
            var data = companyRepository.GetOne(e => e.Id == CompanyId);
            if (data != null)
            {
                return View(data);
            }
            else
            {
                return RedirectToAction("error", "Home");

            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Company company)
        {
            if (ModelState.IsValid)
            {
                companyRepository.Edit(company);
                companyRepository.comit();
                return RedirectToAction(nameof(Index));
            }
            return RedirectToAction("error","Home");




        }


    }
}

