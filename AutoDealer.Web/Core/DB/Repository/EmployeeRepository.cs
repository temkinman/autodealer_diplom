using AutoDealer.Web.Data;
using AutoDealer.Web.Core.DB.Interfaces;
using AutoDealer.Web.Filters;
using AutoDealer.Web.Models;
using System.Collections.Generic;
using System.Linq;

namespace AutoDealer.Web.Core.DB.Repository
{
    public class EmployeeRepository : IEmployeeRepository
    {
        private readonly ApplicationDbContext _dbContext;

        public EmployeeRepository(ApplicationDbContext context)
        {
            _dbContext = context;
        }

        public IQueryable<Employee> Employees => _dbContext.Employees;

        public bool Create(Employee employee)
        {
            _dbContext.Add(employee);
            return _dbContext.SaveChanges() > 0;
        }

        public Employee GetById(int employeeId) => _dbContext.Employees.FirstOrDefault(emp => emp.Id == employeeId);

        public bool Update(Employee employee)
        {
            _dbContext.Update(employee);
            return _dbContext.SaveChanges() > 0;
        }

        public bool Delete(Employee employee)
        {
            _dbContext.Remove(employee);
            return _dbContext.SaveChanges() > 0;
        }

        public Employee GetEmployeeByFullName(string fullName) => _dbContext.Employees.FirstOrDefault(emp => emp.FullName == fullName);

        public Employee GetEmployeeById(int employeeId) => _dbContext.Employees.FirstOrDefault(emp => emp.Id == employeeId);

        public List<Employee> GetEmployeesByFilter(EmployeeFilter filter)
        {
            List<Employee> filtered = new List<Employee>();

            filter = SetFilterIfHasNullProperty(filter);

            foreach (Employee employee in Employees)
            {
                if (IsBDayInRange(employee, filter)) filtered.Add(employee);
            }

            if (filtered.Count == 0) return filtered;

            filtered = filtered.Where(emp => filter.FirstName != null ? emp.FirstName == filter.FirstName : true)
                                .Where(emp => filter.LastName != null ? emp.LastName == filter.LastName : true)
                                .ToList();

            return filtered;
        }

        private EmployeeFilter SetFilterIfHasNullProperty(EmployeeFilter filter)
        {
            filter.BDayFrom ??= System.DateTime.MinValue;
            filter.BDayTo ??= System.DateTime.MaxValue;

            return filter;
        }

        private bool IsBDayInRange(Employee employee, EmployeeFilter filter)
        {
            return employee.BDay >= filter.BDayFrom && employee.BDay <= filter.BDayTo;
        }

        public Employee GetEmployeeByEmail(string email) => _dbContext.Employees.FirstOrDefault(emp => emp.Email == email);
    }
}
