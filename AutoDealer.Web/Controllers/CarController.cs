using AutoDealer.Web.Core.DB.Repository;
using AutoDealer.Web.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using AutoDealer.Web.Filters;
using AutoDealer.Web.ViewModel;
using AutoDealer.Web.Core.DB.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using AutoDealer.Web.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;

namespace AutoDealer.Web.Controllers
{
    public class CarController : Controller
    {
        private readonly ILogger<CarController> _logger;
        private readonly ICarRepository _carRepository;
        private readonly IColorRepository _colorRepository;
        private readonly IModelRepository _modelRepository;
        private readonly ICompanyRepository _companyRepository;
        private readonly ICarOwnerRepository _carownerRepository;
        private readonly IEngineTypeRepository _engineTypeRepository;
        private readonly ITransmissionRepository _transmissionRepository;
        private readonly ISettingsRepository _settingsRepository;
        private readonly IStatusRepository _statusRepository;

        private IDataSource _datasource;
        private IFilter _filter;
        private const int PageSize = 3;
        public CarController(
                             ILogger<CarController> logger,
                             ICarRepository carRepository,
                             IColorRepository colorRepository,
                             IModelRepository modelRepository,
                             ICompanyRepository companyRepository,
                             IEngineTypeRepository engineTypeRepository,
                             ITransmissionRepository transmissionRepository,
                             IServiceProvider serviceProvider,
                             ISettingsRepository settingsRepository,
                             IStatusRepository statusRepository
                             )
        {
            //Singlon object
            _datasource = serviceProvider.GetService<IDataSource>();
            _filter = serviceProvider.GetService<IFilter>();
            _datasource.Update(serviceProvider);

            _logger = logger;
            _carRepository = carRepository;
            _colorRepository = colorRepository;
            _modelRepository = modelRepository;
            _companyRepository = companyRepository;
            _engineTypeRepository = engineTypeRepository;
            _transmissionRepository = transmissionRepository;
            _settingsRepository = settingsRepository;
            _statusRepository = statusRepository;
        }

        private CarFilter _cf = null;

        [Authorize]
        public IActionResult Index(int currentPage = 1, CarFilter carFilter = null, SortState sortOrder = SortState.ModelAsc)
        {

            if (!User.Identity.IsAuthenticated)
            {
                return Content(User.Identity.Name);
                //return View("~/views/account/login.cshtml");
            }

            if (currentPage < 1)
            {
                return NotFound();
                //throw new ArgumentException("Page cannot be less than 1.", nameof(currentPage));
            }

            carFilter.Settings ??= new();

            if (carFilter.IsEmpty && _filter.GetFilter() != null)
            {
                carFilter = _filter.GetFilter();
            }

            if (!carFilter.IsEmpty)
            {
                _filter.SaveFilter(carFilter);
            }

            carFilter.Colors = _datasource.Colors;
            //carFilter.Models = _datasource.Models;
            carFilter.Models = new List<Model>();
            carFilter.Companies = _datasource.Companies;
            carFilter.Engines = _datasource.EngineTypes;
            carFilter.Transmissions = _datasource.Transmissions;

            IQueryable<Car> cars = _carRepository
                .GetCarsByFilter(carFilter);

            switch (sortOrder)
            {
                case SortState.ModelAsc:
                    cars = cars.OrderBy(car => car.Model.Title);
                    break;
                case SortState.PriceAsc:
                    cars = cars.OrderBy(car => car.Price);
                    break;
                case SortState.PriceDesc:
                    cars = cars.OrderByDescending(car => car.Price);
                    break;
                case SortState.YearAsc:
                    cars = cars.OrderBy(car => car.ProduceDate);
                    break;
                case SortState.YearDesc:
                    cars = cars.OrderByDescending(car => car.ProduceDate);
                    break;
                case SortState.KilometreAsc:
                    cars = cars.OrderBy(car => car.Kilometre);
                    break;
                case SortState.KilometreDesc:
                    cars = cars.OrderByDescending(car => car.Kilometre);
                    break;
                case SortState.DateAsc:
                    cars = cars.OrderBy(car => car.ArrivalDate);
                    break;
                case SortState.DateDesc:
                    cars = cars.OrderByDescending(car => car.ArrivalDate);
                    break;
                default:
                    cars = cars.OrderBy(car => car.Model.Title);
                    break;
            }

            IQueryable<Car> splittedByPageCars = cars
                .Skip((currentPage - 1) * PageSize)
                .Take(PageSize);

            PagingInfo pagingInfo = new PagingInfo
            {
                CurrentPage = currentPage,
                PageSize = PageSize,
                TotalItems = cars.Count()
            };

            CarsListViewModel viewModel = new CarsListViewModel
            {
                PagingInfo = pagingInfo,
                Cars = splittedByPageCars.ToList(),
                SortViewModel = new SortViewModel(sortOrder),
                CarFilter = carFilter
            };

            ViewBag.CurrentTab = "cars";

            return View(viewModel);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        [HttpGet]
        public IActionResult FilterCar(CarFilter filter, int currentPage = 1)
        {
            IQueryable<Color> colors = _datasource.Colors;
            IQueryable<Model> models = _datasource.Models;
            IQueryable<Company> companies = _datasource.Companies;
            IQueryable<EngineType> engines = _datasource.EngineTypes;
            IQueryable<Transmission> transmissions = _datasource.Transmissions;

            filter.Colors = colors;
            filter.Engines = engines;
            filter.Models = models;
            filter.Companies = companies;
            filter.Transmissions = transmissions;

            List<Car> allCars = _carRepository
                .GetCarsByFilter(filter)
                .OrderBy(car => car.Id)
                .ToList();

            List<Car> splittedByPageCars = allCars
                .Skip((currentPage - 1) * PageSize)
                .Take(PageSize)
                .ToList();

            PagingInfo pagingInfo = new PagingInfo
            {
                CurrentPage = currentPage,
                PageSize = PageSize,
                TotalItems = allCars.Count()
            };

            CarsListViewModel cars = new CarsListViewModel
            {
                PagingInfo = pagingInfo,
                Cars = splittedByPageCars,
                CarFilter = filter
            };

            return View("Index", cars);
        }

        [HttpGet]
        public RedirectToActionResult ResetSearch()
        {
            _filter.Reset();
            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult CarDetails(int carId, string admin = "")
        {
            Car car = _carRepository.GetById(carId);

            if (admin == "adminPanel")
            {
                ViewBag.Admin = admin;
                return PartialView("ModalDetails", car);
            }

            if (car == null)
            {
                return NotFound();
            }

            return View(car);
        }

        [HttpGet]
        public IActionResult GetModelsByCompany(int companyId, bool filter = false)
        {
            ViewBag.Filter = filter;

            IQueryable<Model> models = _datasource.Models.Where(m => m.Company.Id == companyId);

            AddingUsedCarViewModel viewModel = new AddingUsedCarViewModel { Models = models };

            return PartialView("ModelsByCompany", viewModel);
        }

        [HttpGet]
        public IActionResult CarAdminIndex()
        {
            List<Car> cars = _carRepository.Cars.Where(c => c.Status.Id != (int)CarStatus.Sold)
                                                .OrderBy(c => c.Id)
                                                .ToList();

            return PartialView(cars);
        }

        [HttpPost]
        public IActionResult Delete(int carId)
        {
            Car car = _carRepository.GetById(carId);

            if (car != null)
            {
                _carRepository.Delete(car);
                ViewBag.Success = "deleted";
            }

            List<Car> cars = _carRepository.Cars.OrderBy(c => c.Id).ToList();

            return PartialView("CarAdminIndex", cars);
        }

        [HttpGet]
        public IActionResult CarAdminList()
        {
            List<Car> cars = _carRepository.Cars.OrderBy(c => c.Id).ToList();

            return View(cars);
        }

        [HttpGet]
        public IActionResult Edit(int carId)
        {
            Car car = _carRepository.GetById(carId);

            CarViewModel viewModel = null;

            if (car != null)
            {
                viewModel = new CarViewModel()
                {
                    Car = car,
                    //CarOwner = _carownerRepository.GetById(car.CarOwner.Id),
                    Colors = _datasource.Colors,
                    Companies = _datasource.Companies,
                    EngineTypes = _datasource.EngineTypes,
                    Transmissions = _datasource.Transmissions,
                    Models = _datasource.Models.Where(m => m.Company.Id == car.Company.Id)
                };
            }

            return PartialView(viewModel);
        }

        [HttpPost]
        public IActionResult Edit(CarViewModel model)
        {
            Company сompany = _companyRepository.GetById(model.Car.Company.Id);
            Model carModel = _modelRepository.GetById(model.Car.Model.Id);
            Color color = _colorRepository.GetById(model.Car.Color.Id);
            Status status = _statusRepository.GetById(3);
            EngineType engineType = _engineTypeRepository.GetById(model.Car.Engine.Type.Id);
            Transmission transmission = _transmissionRepository.GetById(model.Car.Transmission.Id);

            Engine engine = new Engine
            {
                Capacity = model.Car.Engine.Capacity,
                Type = engineType
            };

            AdvSettings existSettings = _settingsRepository.Settings.FirstOrDefault(set => set.Equals(model.Car.Settings));

            Car updated = model.Car;
            updated.Company = сompany;
            updated.Model = carModel;
            updated.Color = color;
            updated.Status = status;
            updated.Engine = engine;
            updated.Transmission = transmission;
            //car.CarOwner = carOwner;
            updated.Settings = existSettings ?? model.Car.Settings;

            _carRepository.Update(updated);
            ViewBag.Success = "edited";

            List<Car> cars = _carRepository.Cars.OrderBy(c => c.Id).ToList();

            return PartialView("CarAdminIndex", cars);
        }

        [HttpGet]
        public IActionResult Create()
        {
            Company defaultSelectedCompany = _datasource.Companies.FirstOrDefault();

            CarViewModel viewModel = new CarViewModel()
            {
                Car = new(),
                Colors = _datasource.Colors,
                Companies = _datasource.Companies,
                EngineTypes = _datasource.EngineTypes,
                Transmissions = _datasource.Transmissions,
                Models = _datasource.Models.Where(m => m.Company.Id == defaultSelectedCompany.Id),
                FileUploadModel = new FilePhotoUpload()
            };

            ViewBag.Company = defaultSelectedCompany.Title;
            ViewBag.Model = _datasource.Models.FirstOrDefault(m => m.Company.Id == defaultSelectedCompany.Id).Title;
            ViewBag.CurrentTab = "buyout_car";

            return View(viewModel);
        }

        [HttpPost]
        public IActionResult Create(CarViewModel model)
        {
            Company сompany = _companyRepository.GetById(model.CompanyId);
            Model carModel = _modelRepository.GetById(model.ModelId);
            Color color = _colorRepository.GetById(model.ColorId);
            Status status = _statusRepository.GetById(3);
            EngineType engineType = _engineTypeRepository.GetById(model.EngineTypeId);
            Transmission transmission = _transmissionRepository.GetById(model.TransmissionId);

            Engine engine = new Engine
            {
                Capacity = model.Car.Engine.Capacity,
                Type = engineType
            };

            AdvSettings existSettings = _settingsRepository.Settings.FirstOrDefault(set => set.Equals(model.Car.Settings));

            Car car = model.Car;
            car.Company = сompany;
            car.Model = carModel;
            car.Color = color;
            car.Status = status;
            car.Engine = engine;
            car.Transmission = transmission;
            car.Settings = existSettings ?? model.Car.Settings;

            bool result = _carRepository.Create(car);

            ViewBag.CarSaved = result ? "ok" : "no";

            return View("CarDetails", car);
        }


        //[HttpGet]
        //public IActionResult CompanyIndex()
        //{
        //    IQueryable<Company> companies = _datasource.Companies;
        //    ViewBag.Company = "company";

        //    return PartialView("CompanyIndex", companies);
        //}

        //[HttpGet]
        //public IActionResult CreateCompany()
        //{
        //    return PartialView("CompanyCreate");
        //}

        //[HttpPost]
        //public IActionResult CreateCompany(Company company)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        _companyRepository.Create(company);
        //        IQueryable<Company> companies = _datasource.Companies.OrderBy(car => car.Id);

        //        ViewBag.Success = "created";

        //        return PartialView("CompanyIndex", companies);
        //    }

        //    return PartialView("CompanyCreate", company);
        //}

        //[HttpGet]
        //public IActionResult EditCompany(int companyId)
        //{
        //    Company companyUpdated = _datasource.Companies.Where(c => c.Id == companyId).FirstOrDefault();

        //    if(companyUpdated == null)
        //    {
        //        return NotFound();
        //    }

        //    return PartialView("CompanyEdit", companyUpdated);
        //}

        //[HttpPost]
        //public IActionResult EditCompany(Company company)
        //{

        //    if (ModelState.IsValid)
        //    {
        //        _companyRepository.Update(company);

        //        IQueryable<Company> companies = _datasource.Companies.OrderBy(car => car.Id);
        //        ViewBag.Success = "edited";

        //        return PartialView("CompanyIndex", companies);
        //    }

        //    return PartialView("CompanyEdit", company);
        //}

        //[HttpPost]
        //public IActionResult DeleteCompany(int companyId)
        //{
        //    Company company = _companyRepository.GetById(companyId);

        //    if (company != null)
        //    {
        //        _companyRepository.Delete(company);
        //    }

        //    List<Company> companies = _datasource.Companies.OrderBy(car => car.Id).ToList();
        //    ViewBag.Success = "deleted";

        //    return PartialView("CompanyIndex", companies);
        //}
    }
}
