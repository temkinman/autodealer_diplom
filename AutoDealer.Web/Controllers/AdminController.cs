﻿using Microsoft.AspNetCore.Mvc;

namespace AutoDealer.Web.Controllers
{
    public class AdminController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
