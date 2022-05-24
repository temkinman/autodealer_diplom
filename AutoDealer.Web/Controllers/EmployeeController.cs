using Microsoft.AspNetCore.Mvc;

namespace AutoDealer.Web.Controllers
{
    public class EmployeeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
