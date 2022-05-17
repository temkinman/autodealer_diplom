using AutoDealer.Web.Filters;
using AutoDealer.Web.Models;
using System.Collections.Generic;
using System.Linq;

namespace AutoDealer.Web.Core.DB.Interfaces
{
    public interface IEmployeeRepository : IBaseRepository<Employee>
    {
        IQueryable<Employee> Employees { get; }
        Employee GetEmployeeByFullName(string fullName);
        List<Employee> GetEmployeesByFilter(EmployeeFilter filter);
    }
}
