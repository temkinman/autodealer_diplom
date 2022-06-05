using AutoDealer.Web.Core.DB.Interfaces;
using AutoDealer.Web.Filters;
using AutoDealer.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace AutoDealer.Web.Core.DB.Interfaces
{
    public interface ISaleRepository : IBaseRepository<Sale>
    {
        IQueryable<Sale> Sales { get; }
        List<Sale> GetSalesByEmployee(Employee employee);
        List<Sale> GetSalesByFilter(SaleFilter filter);
        IQueryable<Sale> GetSalesByDate(DateTime? dateFrom, DateTime? dateTo);
    }
}
