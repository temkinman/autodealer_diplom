using AutoDealer.Web.Core.DB.Interfaces;
using AutoDealer.Web.Core.DB.Repository;
using AutoDealer.Web.Models;
using AutoDealer.Web.ViewModel;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;

namespace AutoDealer.Web.Controllers
{
    public class SaleController : Controller
    {
        private readonly ICarRepository _carRepository;
        private readonly ISaleRepository _saleRepository;

        public SaleController(ICarRepository carRepository, ISaleRepository saleRepository)
        {
            _carRepository = carRepository;
            _saleRepository = saleRepository;
        }

        public IActionResult Index(int carId)
        {
            //Car car = _carRepository.GetById(carId);

            List<Sale> sales = _saleRepository.Sales.ToList();

            List<SaleViewModel> viewModelSales = new();

            foreach (Sale sale in sales)
            {
                viewModelSales.Add(new SaleViewModel()
                {
                    Id = sale.Id,
                    CompanyName = sale.Car.Company.Title,
                    ModelName = sale.Car.Model.Title,
                    CustomerFullName = sale.Customer.FullName,
                    Price = sale.FinalPrice,
                    Date = sale.SaledDate
                });
            }

            decimal totalSum = sales.Sum(s => s.FinalPrice);

            SalesViewModel salesResult = new SalesViewModel()
            {
                TotalCars = viewModelSales.Count,
                TotalSum = totalSum,
                Sales = viewModelSales
            };

            return View(salesResult);
        }
    }
}
