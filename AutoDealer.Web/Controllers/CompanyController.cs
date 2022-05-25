using AutoDealer.Web.Core.DB.Interfaces;
using AutoDealer.Web.Models;
using AutoDealer.Web.ViewModel;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;

namespace AutoDealer.Web.Controllers
{
    public class CompanyController : Controller
    {
        private ICompanyRepository _companyRepository;
        private IModelRepository _modelRepository;

        public CompanyController(ICompanyRepository companyRepository, IModelRepository modelRepository)
        {
            _companyRepository = companyRepository;
            _modelRepository = modelRepository;
        }

        [HttpGet]
        public IActionResult GetModelsByCompany(int companyId, bool filter = false)
        {
            ViewBag.Filter = filter;

            IQueryable<Model> models = _modelRepository.Models.Where(m => m.Company.Id == companyId);

            AddingUsedCarViewModel viewModel = new AddingUsedCarViewModel { Models = models };

            return PartialView("ModelsByCompany", viewModel);
        }

        [HttpGet]
        public IActionResult Index()
        {
            IQueryable<Company> companies = _companyRepository.Companies;

            List<string> tabs = new() { "admin_panel" };
            //ViewBag.Tabs = tabs;
            //ViewData["company"] = "company";
            //ViewData["admin_panel"] = "admin_panel";
            //ViewBag.Admin = "admin_panel";
            
            ViewBag.CurrentTab = "company";
            //TempData["currentSubTab"] = "company";

            return PartialView(companies);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return PartialView();
        }

        [HttpPost]
        public IActionResult Create(Company company)
        {
            if (ModelState.IsValid)
            {
                _companyRepository.Create(company);
                IQueryable<Company> companies = _companyRepository.Companies.OrderBy(car => car.Id);

                ViewBag.Success = "created";

                return PartialView("index", companies);
            }

            return PartialView(company);
        }

        [HttpGet]
        public IActionResult Edit(int companyId)
        {
            Company companyUpdated = _companyRepository.Companies.Where(c => c.Id == companyId).FirstOrDefault();

            if (companyUpdated == null)
            {
                return NotFound();
            }

            return PartialView(companyUpdated);
        }

        [HttpPost]
        public IActionResult Edit(Company company)
        {
            if (ModelState.IsValid)
            {
                _companyRepository.Update(company);

                IQueryable<Company> companies = _companyRepository.Companies.OrderBy(car => car.Id);
                ViewBag.Success = "edited";

                return PartialView("index", companies);
            }

            return PartialView(company);
        }

        [HttpPost]
        public IActionResult Delete(int companyId)
        {
            Company company = _companyRepository.GetById(companyId);

            if (company != null)
            {
                _companyRepository.Delete(company);
            }

            List<Company> companies = _companyRepository.Companies.OrderBy(car => car.Id).ToList();
            ViewBag.Success = "deleted";

            return PartialView("index", companies);
        }
    }
}
