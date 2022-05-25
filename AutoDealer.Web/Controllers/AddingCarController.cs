using AutoDealer.Web.Core.DB.Interfaces;
using AutoDealer.Web.Models;
using AutoDealer.Web.ViewModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;

namespace AutoDealer.Web.Controllers
{
    public class AddingCarController : Controller
    {
        private readonly ICarRepository _carRepository;
        private readonly ICarOwnerRepository _carOwnerRepository;
        private readonly IColorRepository _colorRepository;
        private readonly IModelRepository _modelRepository;
        private readonly ICompanyRepository _companyRepository;
        private readonly IEngineTypeRepository _engineTypeRepository;
        private readonly ITransmissionRepository _transmissionRepository;
        private readonly ISettingsRepository _settingsRepository;
        private readonly IStatusRepository _statusRepository;
        private IDataSource _datasource;
        private AddingUsedCarViewModel _addingUsedCarViewModel;

        public AddingCarController(ICarRepository carRepository,
                                    IServiceProvider serviceProvider,
                                    ICarOwnerRepository carOwnerRepository,
                                    IColorRepository colorRepository,
                                    IModelRepository modelRepository,
                                    ICompanyRepository companyRepository,
                                    IEngineTypeRepository engineTypeRepository,
                                    ITransmissionRepository transmissionRepository,
                                    ISettingsRepository settingsRepository,
                                    IStatusRepository statusRepository
                                    )
        {
            _datasource = serviceProvider.GetService<IDataSource>();
            _datasource.Update(serviceProvider);

            _carRepository = carRepository;
            _carOwnerRepository = carOwnerRepository;
            _colorRepository = colorRepository;
            _modelRepository = modelRepository;
            _companyRepository = companyRepository;
            _engineTypeRepository = engineTypeRepository;
            _transmissionRepository = transmissionRepository;
            _settingsRepository = settingsRepository;
            _statusRepository = statusRepository;

            _addingUsedCarViewModel = new AddingUsedCarViewModel
            {
                Auto = new(),
                CarOwner = new(),
                Colors = _datasource.Colors,
                Companies = _datasource.Companies,
                EngineTypes = _datasource.EngineTypes,
                Transmissions = _datasource.Transmissions,
                Models = _datasource.Models
            };
        }

        public IActionResult Index()
        {
            return View("AddingOwner");
        }

        [HttpPost]
        public IActionResult AddOwner([FromForm] CarOwner model)
        {
            if (!ModelState.IsValid)
            {
                //Response.StatusCode = (int)HttpStatusCode.BadRequest;
                return BadRequest("Model is not valid");
            }

            CarOwner carOwnerCreated = _carOwnerRepository.CarOwners.FirstOrDefault(own => own.Equals(model));

            if (carOwnerCreated == null)
            {
                bool result = _carOwnerRepository.Create(model);
                _addingUsedCarViewModel.CarOwnerId = model.Id;

                ViewBag.OwnerSaved = result ? "ok" : "no";
                ViewBag.OwnerCreated = "no";
            }
            else
            {
                _addingUsedCarViewModel.CarOwnerId = carOwnerCreated.Id;
                ViewBag.OwnerCreated = "ok";
            }

            _addingUsedCarViewModel.CarOwnerId = model.Id;
            _addingUsedCarViewModel.CarOwner = model;

            return View("AddingCar", _addingUsedCarViewModel);
        }

        [HttpPost]
        public IActionResult AddCar([FromForm] AddingUsedCarViewModel model)
        {
            CarOwner carOwner = _carOwnerRepository.GetById(model.CarOwnerId);

            Company сompany = _companyRepository.GetById(model.CompanyId);
            Model carModel = _modelRepository.GetById(model.ModelId);
            Color color = _colorRepository.GetById(model.ColorId);
            Status status = _statusRepository.GetById(3);
            EngineType engineType = _engineTypeRepository.GetById(model.EngineTypeId);
            Transmission transmission = _transmissionRepository.GetById(model.TransmissionId);

            Engine engine = new Engine
            {
                Capacity = model.Auto.Engine.Capacity,
                Type = engineType
            };

            AdvSettings existSettings = _settingsRepository.Settings.FirstOrDefault(set => set.Equals(model.Auto.Settings));

            Car car = model.Auto;
            car.Company = сompany;
            car.Model = carModel;
            car.Color = color;
            car.Status = status;
            car.Engine = engine;
            car.Transmission = transmission;
            car.CarOwner = carOwner;
            car.Settings = existSettings ?? model.Auto.Settings;

            bool result = _carRepository.Create(car);

            _addingUsedCarViewModel.Auto = car;
            _addingUsedCarViewModel.CarOwner = carOwner;

            ViewBag.CarSaved = result ? "ok" : "no";

            return View("AddingCarFinish", _addingUsedCarViewModel);
        }

        [HttpGet]
        public IActionResult AddSingleCar()
        {
            Dictionary<int, List<Model>> modelsDictionary = new();//_dbContext.Models.GroupBy(m => m.Company.Id).Select(n => n.First()).ToDictionary(c => c.);


            foreach (Model model in _datasource.Models)
            {
                if (!modelsDictionary.ContainsKey(model.Company.Id))
                    modelsDictionary.Add(model.Company.Id, new List<Model>());

                modelsDictionary[model.Company.Id].Add(model);
            }

            var jsonModels = JsonConvert.SerializeObject(modelsDictionary);

            ViewBag.Models = jsonModels;

            return View(_addingUsedCarViewModel);
        }

        [HttpPost]
        public IActionResult AddSingleCar([FromForm] AddingUsedCarViewModel model)
        {
            Company сompany = _companyRepository.GetById(model.CompanyId);
            Model carModel = _modelRepository.GetById(model.ModelId);
            Color color = _colorRepository.GetById(model.ColorId);
            Status status = _statusRepository.GetById(3);
            EngineType engineType = _engineTypeRepository.GetById(model.EngineTypeId);
            Transmission transmission = _transmissionRepository.GetById(model.TransmissionId);

            Engine engine = new Engine
            {
                Capacity = model.Auto.Engine.Capacity,
                Type = engineType
            };

            AdvSettings existSettings = _settingsRepository.Settings.FirstOrDefault(set => set.Equals(model.Auto.Settings));

            Car car = model.Auto;
            car.Company = сompany;
            car.Model = carModel;
            car.Color = color;
            car.Status = status;
            car.Engine = engine;
            car.Transmission = transmission;
            car.Settings = existSettings ?? model.Auto.Settings;

            bool result = _carRepository.Create(car);

            _addingUsedCarViewModel.Auto = car;

            ViewBag.CarSaved = result ? "ok" : "no";
            ViewBag.Models = "";


            return View(_addingUsedCarViewModel);
        }
    }
}
