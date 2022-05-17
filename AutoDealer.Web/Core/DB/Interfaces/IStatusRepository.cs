using AutoDealer.Web.Models;
using System.Linq;

namespace AutoDealer.Web.Core.DB.Interfaces
{
    public interface IStatusRepository : IBaseRepository<Status>
    {
        IQueryable<Status> Statuses { get; }
    }
}
