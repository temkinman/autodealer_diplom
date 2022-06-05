using AutoDealer.Web.Data;
using AutoDealer.Web.Filters;
using AutoDealer.Web.Models;
using System.Collections.Generic;
using System.Linq;
using AutoDealer.Web.Core.DB.Interfaces;
using System;

namespace AutoDealer.Web.Core.DB.Repository
{
    public class SaleRepository : ISaleRepository
    {
        private readonly ApplicationDbContext _dbContext;

        public SaleRepository(ApplicationDbContext context)
        {
            _dbContext = context;
        }

        public IQueryable<Sale> Sales => _dbContext.Sales;

        public bool Create(Sale sale)
        {
            _dbContext.Add(sale);
            return _dbContext.SaveChanges() > 0;
        }

        public Sale GetById(int saleId) => _dbContext.Sales.FirstOrDefault(s => s.Id == saleId);

        public bool Update(Sale sale)
        {
            _dbContext.Update(sale);
            return _dbContext.SaveChanges() > 0;
        }

        public bool Delete(Sale sale)
        {
            _dbContext.Remove(sale);
            return _dbContext.SaveChanges() > 0;
        }

        public List<Sale> GetSalesByEmployee(Employee employee) => _dbContext.Sales.Where(s => s.Employee.FullName == employee.FullName).ToList();

        public List<Sale> GetSalesByFilter(SaleFilter filter)
        {
            List<Sale> filtered = new List<Sale>();

            filter = SetFilterIfHasNullProperty(filter);

            foreach (Sale sale in Sales)
            {
                if (
                    IsSaleDateInRange(sale, filter)
                    && IsSalePriceInRange(sale, filter)
                    )
                {
                    filtered.Add(sale);
                }
            }

            if (filtered.Count == 0) return filtered;

            filtered = filtered.Where(sale => sale.Employee.FullName == filter.Employee.FullName)
                                .Where(sale => sale.Customer.Equals(filter.Customer))
                                .Where(sale => sale.Car.Equals(filter.Car))
                                .ToList();

            return filtered;
        }

        private SaleFilter SetFilterIfHasNullProperty(SaleFilter filter)
        {
            filter.SaleDateFrom ??= System.DateTime.MinValue;
            filter.SaleDateTo ??= System.DateTime.MaxValue;

            filter.PriceFrom ??= 0;
            filter.PriceTo ??= int.MaxValue;

            return filter;
        }
        private bool IsSaleDateInRange(Sale sale, SaleFilter filter)
        {
            return sale.SaledDate >= filter.SaleDateFrom && sale.SaledDate <= filter.SaleDateTo;
        }

        private bool IsSalePriceInRange(Sale sale, SaleFilter filter)
        {
            return sale.FinalPrice >= filter.PriceFrom && sale.FinalPrice <= filter.PriceTo;
        }

        public IQueryable<Sale> GetSalesByDate(DateTime? dateFrom, DateTime? dateTo)
        {
            dateFrom ??= DateTime.MinValue;
            dateTo = dateTo == null ? DateTime.MaxValue : dateTo.Value.AddDays(1);

            return _dbContext.Sales.Where(s => dateFrom <= s.SaledDate && s.SaledDate <= dateTo);
        }
    }
}
