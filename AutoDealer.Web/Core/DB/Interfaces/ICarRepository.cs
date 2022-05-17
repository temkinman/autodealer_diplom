using AutoDealer.Web.Core.DB.Interfaces;
using AutoDealer.Web.Enums;
using AutoDealer.Web.Filters;
using AutoDealer.Web.Models;
using System.Collections.Generic;
using System.Linq;

namespace AutoDealer.Web.Core.DB.Interfaces
{
    public interface ICarRepository : IBaseRepository<Car>
    {
        IQueryable<Car> Cars { get; }
        IQueryable<Car> GetAllCarsByCompany(Company company);
        IQueryable<Car> GetAllCarsByModel(Model model);
        IQueryable<Car> GetAllCarsByAdvSettings(AdvSettings settings);
        IQueryable<Car> GetCarsByFilter(CarFilter filter, int status = (int)CarStatus.Opened);
    }
}
