﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace AutoDealer.Web.Controllers
{
    public class AdminController : Controller
    {
        [Authorize]
        public IActionResult Index()
        {
            if (!User.Identity.IsAuthenticated)
            {
                return Content(User.Identity.Name);
            }

            ViewBag.CurrentTab = "admin_panel";

            return View();
        }

        public IActionResult CarListIndex()
        {

            return View("~/Views/Car/CarAdminList.cshtml");
        }
    }
}
