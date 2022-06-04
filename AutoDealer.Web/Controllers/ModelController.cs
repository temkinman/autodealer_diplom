using AutoDealer.Web.Core.DB.Interfaces;
using AutoDealer.Web.Models;
using AutoDealer.Web.ViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace AutoDealer.Web.Controllers
{
    [Authorize]
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
        public IActionResult Models(int companyId)
        {
            IQueryable<Model> models = _modelRepository.Models.Where(m => m.Company.Id == companyId);

            CarModelViewModel viewModel = new()
            {
                Models = models,
                CompanyId = companyId
            };

            return PartialView(viewModel);
        }

        [HttpGet]
        public IActionResult Create(int companyId)
        {
            if (companyId > 0)
            {
                Company company = _companyRepository.GetById(companyId);

                Model viewModel = new()
                {
                    Company = company
                };
                return PartialView(viewModel);
            }

            IQueryable<Model> models = _modelRepository.Models.Where(m => m.Company.Id == companyId);

            CarModelViewModel viewModels = new()
            {
                Models = models
            };

            return PartialView("Models", viewModels);
        }

        [HttpPost]
        public IActionResult Create([FromForm] Model model)
        {
            if (model.Company.Id > 0)
            {
                Company company = _companyRepository.GetById(model.Company.Id);
                model.Company = company;
            }
            
            if (ModelState.IsValid)
            {
                Company company = _companyRepository.GetById(model.Company.Id);

                model.Company = company;
                _modelRepository.Create(model);

                IQueryable<Model> models = _modelRepository.Models.Where(m => m.Company.Id == model.Company.Id);

                CarModelViewModel viewModel = new()
                {
                    Models = models
                };

                ViewBag.Success = "created";

                return PartialView("Models", viewModel);
            }

            return PartialView(model);
        }


        [HttpGet]
        public IActionResult Edit(int modelId)
        {
            Model model = _modelRepository.GetById(modelId);

            return PartialView(model);
        }

        [HttpPost]
        public IActionResult Edit(Model model)
        {
            if (ModelState.IsValid)
            {
                Company company = _companyRepository.GetById(model.Company.Id);

                model.Company = company;
                _modelRepository.Update(model);

                IQueryable<Model> models = _modelRepository.Models.Where(m => m.Company.Id == model.Company.Id);

                CarModelViewModel viewModels = new()
                {
                    Models = models
                };

                ViewBag.Success = "edited";

                return PartialView("Models", viewModels);
            }

            return PartialView(model);
        }

        [HttpPost]
        public IActionResult Delete(int modelId)
        {
            Model model = _modelRepository.GetById(modelId);

            if (model != null)
            {
                _modelRepository.Delete(model);
            }

            IQueryable<Model> models = _modelRepository.Models.Where(m => m.Company.Id == model.Company.Id);

            CarModelViewModel viewModel = new()
            {
                Models = models
            };

            ViewBag.Success = "deleted";

            return PartialView("Models", viewModel);
        }
    }
}
