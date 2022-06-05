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
        private const int PageSize = 5;
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

        //private CarFilter _cf = null;

        
        public IActionResult Index(int currentPage = 1, CarFilter carFilter = null, SortState sortOrder = SortState.ModelAsc)
        {
            if (currentPage < 1)
            {
                return NotFound();
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
            carFilter.Models = carFilter.CompanyId > 0 ? _datasource.Models.Where(m => m.Company.Id == carFilter.CompanyId) : new List<Model>();
            carFilter.Companies = _datasource.Companies;
            carFilter.Engines = _datasource.EngineTypes;
            carFilter.Transmissions = _datasource.Transmissions;

            IQueryable<Car> cars = _carRepository.GetCarsByFilter(carFilter);

            switch (sortOrder)
            {
                case SortState.PriceAsc:
                    cars = cars.OrderBy(car => car.Price);
                    ViewBag.Asc = "price";
                    break;
                case SortState.PriceDesc:
                    cars = cars.OrderByDescending(car => car.Price);
                    ViewBag.Desc = "price";
                    break;
                case SortState.YearAsc:
                    cars = cars.OrderBy(car => car.ProduceDate);
                    ViewBag.Asc = "year";
                    break;
                case SortState.YearDesc:
                    cars = cars.OrderByDescending(car => car.ProduceDate);
                    ViewBag.Desc = "year";
                    break;
                case SortState.DateAsc:
                    cars = cars.OrderBy(car => car.ArrivalDate);
                    ViewBag.Asc = "date";
                    break;
                case SortState.DateDesc:
                    cars = cars.OrderByDescending(car => car.ArrivalDate);
                    ViewBag.Desc = "date";
                    break;
                default:
                    cars = cars.OrderBy(car => car.Model.Title);
                    break;
            }

            IQueryable<Car> splittedByPageCars = cars
                .Where(c => c.Status.Id != (int)CarStatus.Sold)
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

            ViewBag.CurrentTab = "cars";

            return View(car);
        }

        [HttpGet]
        public IActionResult GetModelsByCompany(int companyId, bool filter = false, bool editCar = false)
        {
            ViewBag.Filter = filter;
            ViewBag.EditCar = editCar;

            IQueryable<Model> models = _datasource.Models.Where(m => m.Company.Id == companyId);

            AddingUsedCarViewModel viewModel = new AddingUsedCarViewModel { Models = models };

            return PartialView("ModelsByCompany", viewModel);
        }

        [HttpGet]
        [Authorize]
        public IActionResult CarAdminIndex()
        {
            List<Car> cars = _carRepository.Cars.Where(c => c.Status.Id != (int)CarStatus.Sold)
                                                .OrderBy(c => c.Id)
                                                .ToList();

            ViewBag.CurrentTab = "admin";
            ViewBag.CarAdmin = "car_admin";

            return PartialView(cars);
        }

        [HttpPost]
        [Authorize]
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
        [Authorize]
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
        [Authorize]
        public IActionResult Edit(CarViewModel model)
        {
            Company сompany = _companyRepository.GetById(model.Car.Company.Id);

            int carModelId = model.ModelId != 0 ? model.ModelId : model.Car.Model.Id;
            Model carModel = _modelRepository.GetById(carModelId);
            Color color = _colorRepository.GetById(model.Car.Color.Id);
            Status status = _statusRepository.GetById((int)CarStatus.Opened);
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
        [Authorize]
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
        [Authorize]
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
    }
}
