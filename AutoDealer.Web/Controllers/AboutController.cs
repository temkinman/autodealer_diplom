using Microsoft.AspNetCore.Mvc;

namespace AutoDealer.Web.Controllers
{
    public class AboutController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
