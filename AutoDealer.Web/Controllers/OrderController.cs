using AutoDealer.Web.Core.DB.Interfaces;
using AutoDealer.Web.Core.DB.Repository;
using AutoDealer.Web.Enums;
using AutoDealer.Web.Models;
using AutoDealer.Web.ViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace AutoDealer.Web.Controllers
{
    [Authorize]
    public class OrderController : Controller
    {
        private readonly ICarRepository _carRepository;
        private readonly ISaleRepository _saleRepository;
        private readonly IStatusRepository _statusRepository;
        private readonly IEmployeeRepository _employeeRepository;
        private IDataSource _datasource;

        public OrderController(ICarRepository carRepository,
                                IServiceProvider serviceProvider,
                                ISaleRepository saleRepository,
                                IStatusRepository statusRepository,
                                IEmployeeRepository employeeRepository)
        {
            _carRepository = carRepository;
            _datasource = serviceProvider.GetService<IDataSource>();
            _datasource.Update(serviceProvider);
            _saleRepository = saleRepository;
            _statusRepository = statusRepository;
            _employeeRepository = employeeRepository;
        }

        public IActionResult Index(int carId)
        {
            return View();
        }

        [HttpGet]
        public IActionResult Create(int carId)
        {
            Car auto = _carRepository.GetById(carId);

            OrderViewModel orderViewModel = new OrderViewModel
            {
                Auto = auto,
                Client = new(),
                AutoId = auto.Id
            };

            return View(orderViewModel);
        }

        [HttpPost]
        public IActionResult Create([FromForm] OrderViewModel orderViewModel)
        {
            Car auto = _carRepository.GetById(orderViewModel.AutoId);

            Employee employee = _employeeRepository.GetEmployeeByEmail(User.Identity.Name);

            if (auto == null)
            {
                return NotFound();
            }

            Sale sale = new Sale
            {
                Car = auto,
                Customer = orderViewModel.Client,
                FinalPrice = orderViewModel.FinalPrice,
                Employee = employee
            };

            try
            {
                bool result = _saleRepository.Create(sale);

                if (result)
                {
                    Status status = _statusRepository.GetById((int)CarStatus.Sold);
                    auto.Status = status;
                    _carRepository.Update(auto);

                    ViewBag.SaleSuccess = "ok";
                }
            }
            catch (Exception)
            {

                ViewBag.SaleSuccess = "no";
            }

            return View("OrderResult", sale);
        }

    }
}
