using AutoDealer.Web.Models;
using System.Linq;

namespace AutoDealer.Web.Core.DB.Interfaces
{
    public interface ITransmissionRepository : IBaseRepository<Transmission>
    {
        IQueryable<Transmission> Transmissions { get; }
    }
}
