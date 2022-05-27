using AutoDealer.Web.Core.DB.Interfaces;
using AutoDealer.Web.Core.DB.Repository;
using AutoDealer.Web.Enums;
using AutoDealer.Web.Models;
using AutoDealer.Web.ViewModel;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;

namespace AutoDealer.Web.Controllers
{
    public class SaleController : Controller
    {
        private readonly ICarRepository _carRepository;
        private readonly ISaleRepository _saleRepository;
        private const int pageSize = 2;

        public SaleController(ICarRepository carRepository, ISaleRepository saleRepository)
        {
            _carRepository = carRepository;
            _saleRepository = saleRepository;
        }

        public IActionResult Index(DateTime? dateFrom = null, DateTime? dateTo = null, string selectedCompany = "", int currentPage = 1, SortState sortOrder = SortState.DateAsc)
        {
            //Car car = _carRepository.GetById(carId);

            IQueryable<Sale> sales = _saleRepository.Sales;

            switch (sortOrder)
            {
                case SortState.ModelAsc:
                    sales = sales.OrderBy(sale => sale.Car.Model.Title);
                    break;
                case SortState.ModelDesc:
                    sales = sales.OrderByDescending(sale => sale.Car.Model.Title);
                    break;
                case SortState.PriceAsc:
                    sales = sales.OrderBy(sale => sale.FinalPrice);
                    break;
                case SortState.PriceDesc:
                    sales = sales.OrderByDescending(sale => sale.FinalPrice);
                    break;
                case SortState.CompanyAsc:
                    sales = sales.OrderBy(sale => sale.Car.Company.Title);
                    break;
                case SortState.CompanyDesc:
                    sales = sales.OrderByDescending(sale => sale.Car.Company.Title);
                    break;
                case SortState.CustomerAsc:
                    sales = sales.OrderBy(sale => sale.Customer.LastName);
                    break;
                case SortState.CustomerDesc:
                    sales = sales.OrderByDescending(sale => sale.Customer.LastName);
                    break;
                case SortState.DateDesc:
                    sales = sales.OrderByDescending(sale => sale.SaledDate);
                    break;
                default:
                    sales = sales.OrderBy(sale => sale.SaledDate);
                    break;
            }

            IQueryable<Sale> splittedByPageSales = sales
                .Skip((currentPage - 1) * pageSize)
                .Take(pageSize);

            PagingInfo pagingInfo = new PagingInfo
            {
                CurrentPage = currentPage,
                PageSize = pageSize,
                TotalItems = sales.Count()
            };

            List<SaleViewModel> viewModelSales = splittedByPageSales.Select(sale => new SaleViewModel()
            {
                Id = sale.Id,
                CompanyName = sale.Car.Company.Title,
                ModelName = sale.Car.Model.Title,
                CustomerFullName = sale.Customer.FullName,
                Price = sale.FinalPrice,
                Date = sale.SaledDate
            }).ToList();

            decimal totalSum = viewModelSales.Sum(s => s.Price);

            SalesListViewModel viewModels = new SalesListViewModel
            {
                PagingInfo = pagingInfo,
                Sales = viewModelSales,
                SortViewModel = new SortViewModel(sortOrder),
                DateFrom = dateFrom ?? DateTime.MinValue,
                DateTo = dateTo ?? DateTime.MaxValue,
                SelectedCompany = selectedCompany,
                TotalCars = sales.Count(),
                TotalSum = totalSum,
            };

            //SalesViewModel salesResult = new SalesViewModel()
            //{
            //    TotalCars = viewModelSales.Count,
            //    TotalSum = totalSum,
            //    Sales = viewModelSales
            //};

            return View(viewModels);
        }
    }
}
