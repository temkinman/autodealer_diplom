using AutoDealer.Web.Models;
using System.Linq;

namespace AutoDealer.Web.Core.DB.Interfaces
{
    public interface ICompanyRepository : IBaseRepository<Company>
    {
        IQueryable<Company> Companies { get; }


    }
}
