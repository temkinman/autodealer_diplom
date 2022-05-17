using AutoDealer.Web.Filters;
using AutoDealer.Web.Models;
using System.Collections.Generic;
using System.Linq;

namespace AutoDealer.Web.Core.DB.Interfaces
{
    public interface ICarOwnerRepository : IBaseRepository<CarOwner>
    {
        IQueryable<CarOwner> CarOwners { get; }
    }
}
