using AutoDealer.Web.Core.DB.Interfaces;
using AutoDealer.Web.Core.DB.Repository;
using AutoDealer.Web.Enums;
using AutoDealer.Web.Models;
using AutoDealer.Web.ViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;

namespace AutoDealer.Web.Controllers
{
    [Authorize]
    public class SaleController : Controller
    {
        private readonly ICarRepository _carRepository;
        private readonly ISaleRepository _saleRepository;
        private readonly IEmployeeRepository _employeeRepository;
        private const int pageSize = 5;

        public SaleController(ICarRepository carRepository,
                                ISaleRepository saleRepository,
                                IEmployeeRepository employeeRepository)
        {
            _carRepository = carRepository;
            _saleRepository = saleRepository;
            _employeeRepository = employeeRepository;
        }

        public IActionResult Index(DateTime? dateFrom = null, DateTime? dateTo = null, int currentPage = 1, SortState sortOrder = SortState.DateAsc)
        {
            IQueryable<Sale> sales = null;

            if(dateFrom != null || dateTo != null)
            {
                sales = _saleRepository.GetSalesByDate(dateFrom, dateTo);
            }
            else
            {
                sales = _saleRepository.Sales;
            }

            switch (sortOrder)
            {
                case SortState.ModelAsc:
                    sales = sales.OrderBy(sale => sale.Car.Model.Title);
                    ViewBag.Asc = "model";
                    break;
                case SortState.ModelDesc:
                    sales = sales.OrderByDescending(sale => sale.Car.Model.Title);
                    ViewBag.Desc = "model";
                    break;
                case SortState.PriceAsc:
                    sales = sales.OrderBy(sale => sale.FinalPrice);
                    ViewBag.Asc = "price";
                    break;
                case SortState.PriceDesc:
                    sales = sales.OrderByDescending(sale => sale.FinalPrice);
                    ViewBag.Desc = "price";
                    break;
                case SortState.CompanyAsc:
                    sales = sales.OrderBy(sale => sale.Car.Company.Title);
                    ViewBag.Asc = "company";
                    break;
                case SortState.CompanyDesc:
                    sales = sales.OrderByDescending(sale => sale.Car.Company.Title);
                    ViewBag.Desc = "company";
                    break;
                case SortState.CustomerAsc:
                    sales = sales.OrderBy(sale => sale.Customer.LastName);
                    ViewBag.Asc = "customer";
                    break;
                case SortState.CustomerDesc:
                    sales = sales.OrderByDescending(sale => sale.Customer.LastName);
                    ViewBag.Desc = "customer";
                    break;
                case SortState.DateDesc:
                    sales = sales.OrderByDescending(sale => sale.SaledDate);
                    ViewBag.Desc = "date";
                    break;
                default:
                    sales = sales.OrderBy(sale => sale.SaledDate);
                    ViewBag.Asc = "date";
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
                Date = sale.SaledDate,
                EmployeeFullName = sale.Employee.FullName
            }).ToList();

            decimal totalSum = viewModelSales.Sum(s => s.Price);

            SalesListViewModel viewModels = new SalesListViewModel
            {
                PagingInfo = pagingInfo,
                Sales = viewModelSales,
                SortViewModel = new SortViewModel(sortOrder),
                TotalCars = sales.Count(),
                TotalSum = totalSum,
                DateFrom = dateFrom,
                DateTo = dateTo
            };

            ViewBag.CurrentTab = "sales";

            return View(viewModels);
        }

        [HttpGet]
        public IActionResult Details(int saleId)
        {
            Sale sale = _saleRepository.GetById(saleId);
            

            if (sale == null)
            {
                return NotFound();
            }

            ViewBag.CurrentTab = "sales";

            return PartialView("Details", sale);
        }
    }
}
