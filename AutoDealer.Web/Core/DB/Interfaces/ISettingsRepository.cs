using AutoDealer.Web.Models;
using System.Linq;

namespace AutoDealer.Web.Core.DB.Interfaces
{
    public interface ISettingsRepository : IBaseRepository<AdvSettings>
    {
        IQueryable<AdvSettings> Settings { get; }
    }
}
