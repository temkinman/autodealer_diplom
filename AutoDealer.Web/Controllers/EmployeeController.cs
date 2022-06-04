using AutoDealer.Web.Core.DB.Interfaces;
using AutoDealer.Web.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace AutoDealer.Web.Controllers
{
    [Authorize]
    public class EmployeeController : Controller
    {
        private IEmployeeRepository _employeeRepository;

        public EmployeeController(IEmployeeRepository employeeRepository)
        {
            _employeeRepository = employeeRepository;
        }

        public IActionResult Index()
        {
            IQueryable<Employee> employees = _employeeRepository.Employees.OrderBy(c => c.LastName);

            return PartialView(employees);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return PartialView();
        }

        [HttpPost]
        public IActionResult Create(Employee model)
        {
            if (ModelState.IsValid)
            {
                _employeeRepository.Create(model);

                IQueryable<Employee> employees = _employeeRepository.Employees.OrderBy(c => c.LastName);
                ViewBag.Success = "created";

                return PartialView("Index", employees);
            }

            return PartialView(model);
        }


        [HttpGet]
        public IActionResult Edit(int employeeId)
        {
            Employee model = _employeeRepository.GetById(employeeId);

            return PartialView(model);
        }

        [HttpPost]
        public IActionResult Edit(Employee model)
        {
            if (ModelState.IsValid)
            {
                _employeeRepository.Update(model);

                IQueryable<Employee> employees = _employeeRepository.Employees.OrderBy(c => c.LastName);
                ViewBag.Success = "edited";

                return PartialView("Index", employees);
            }

            return PartialView(model);
        }

        [HttpPost]
        public IActionResult Delete(int employeeId)
        {
            Employee model = _employeeRepository.GetById(employeeId);

            if (model != null)
            {
                _employeeRepository.Delete(model);
            }

            IQueryable<Employee> employees = _employeeRepository.Employees.OrderBy(c => c.LastName);

            ViewBag.Success = "deleted";

            return PartialView("Index", employees);
        }
    }
}
