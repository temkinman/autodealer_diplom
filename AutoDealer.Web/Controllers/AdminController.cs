using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace AutoDealer.Web.Controllers
{
    public class AdminController : Controller
    {
        [Authorize]
        public IActionResult Index()
        {
            if (!User.Identity.IsAuthenticated || User.Identity.Name != "admin@dealer.com")
            {
                ViewBag.NeedToBeAdmin = "true";
                ViewBag.AdminLoginMessage = "Необходимы права администратора";
                return View("~/Views/Account/Login.cshtml");
            }

            ViewBag.CurrentTab = "admin";

            return View();
        }
    }
}
