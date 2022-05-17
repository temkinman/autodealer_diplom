using AutoDealer.Web.Models;
using System.Linq;

namespace AutoDealer.Web.Core.DB.Interfaces
{
    public interface ICustomerRepository : IBaseRepository<Customer>
    {
        IQueryable<Customer> Customers { get; }
    }
}
