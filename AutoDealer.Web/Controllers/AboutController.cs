using Microsoft.AspNetCore.Mvc;

namespace AutoDealer.Web.Controllers
{
    public class AboutController : Controller
    {
        public IActionResult Index()
        {
            ViewBag.CurrentTab = "about";
            return View();
        }
    }
}
