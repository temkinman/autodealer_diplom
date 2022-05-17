using AutoDealer.Web.Models;
using System.Linq;

namespace AutoDealer.Web.Core.DB.Interfaces
{
    public interface IEngineRepository : IBaseRepository<Engine>
    {
        IQueryable<Engine> Engines { get; }
    }
}
