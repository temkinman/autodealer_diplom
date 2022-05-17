using AutoDealer.Web.Core.DB.Interfaces;
using AutoDealer.Web.Models;
using System.Linq;

namespace AutoDealer.Web.Core.DB.Interfaces
{
    public interface IEngineTypeRepository : IBaseRepository<EngineType>
    {
        IQueryable<EngineType> EngineTypes { get; }
    }
}
