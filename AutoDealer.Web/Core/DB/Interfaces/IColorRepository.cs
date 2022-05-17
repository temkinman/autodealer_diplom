using AutoDealer.Web.Models;
using System.Linq;

namespace AutoDealer.Web.Core.DB.Interfaces
{
    public interface IColorRepository : IBaseRepository<Color>
    {
        IQueryable<Color> Colors { get; }
    }
}
