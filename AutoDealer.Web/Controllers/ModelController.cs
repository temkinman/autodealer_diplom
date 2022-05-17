using AutoDealer.Web.Core.DB.Interfaces;
using AutoDealer.Web.Models;
using AutoDealer.Web.ViewModel;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace AutoDealer.Web.Controllers
{
    public class ModelController : Controller
    {
        private readonly IModelRepository _modelRepository;
        private readonly ICompanyRepository _companyRepository;

        public ModelController(IModelRepository modelRepository, ICompanyRepository companyRepository)
        {
            _modelRepository = modelRepository;
            _companyRepository = companyRepository;
        }

        public IActionResult Index()
        {
            IQueryable<Company> companies = _companyRepository.Companies.OrderBy(c => c.Title);

            CarModelViewModel model = new()
            {
                Companies = companies
            };

            return PartialView(model);
        }

        [HttpGet]
        public IActionResult FilterByCompany(int companyId)
        {
            IQueryable<Model> models = _modelRepository.Models.Where(m => m.Company.Id == companyId);

            CarModelViewModel viewModel = new()
            {
                Models = models
            };

            return PartialView(viewModel);
        }

        [HttpGet]
        public IActionResult Create(int companyId)
        {
            CarModelViewModel model = new()
            {
                CompanyId = companyId
            };
            return PartialView("Create", model);
        }

        [HttpPost]
        public IActionResult Create(CarModelViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                Company company = _companyRepository.GetById(viewModel.CompanyId);

                viewModel.Model.Company = company;
                _modelRepository.Create(viewModel.Model);

                //IQueryable<Model> companies = _datasource.Companies.OrderBy(car => car.Id);

                //return PartialView("Index", companies);
            }

            return NotFound();
        }

        [HttpGet]
        public IActionResult Edit(int companyId)
        {
            //Company companyUpdated = _datasource.Companies.Where(c => c.Id == companyId).FirstOrDefault();

            //if (companyUpdated == null)
            //{
            //    return NotFound();
            //}
            return View();
            //return PartialView("CompanyEdit", companyUpdated);
        }

        [HttpPost]
        public IActionResult Edit(Model model)
        {

            //if (ModelState.IsValid)
            //{
            //    _companyRepository.Update(company);
            //}

            //IQueryable<Company> companies = _datasource.Companies.OrderBy(car => car.Id);
            //ViewBag.Success = "ok";

            //return PartialView("CompanyIndex", companies);

            return View();
        }

        [HttpPost]
        public IActionResult Delete(int modelId, int companyId)
        {
            Model model = _modelRepository.GetById(modelId);

            if (model != null)
            {
                _modelRepository.Delete(model);
            }

            IQueryable<Model> models = _modelRepository.Models.Where(m => m.Company.Id == companyId);

            CarModelViewModel viewModel = new()
            {
                Models = models
            };

            return PartialView("Index", viewModel);
        }
    }
}
