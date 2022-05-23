using AutoDealer.Web.Core.DB.Interfaces;
using AutoDealer.Web.Models;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace AutoDealer.Web.Controllers
{
    public class ColorController : Controller
    {
        private IColorRepository _colorRepository;

        public ColorController(IColorRepository colorRepository)
        {
            _colorRepository = colorRepository;
        }

        public IActionResult Index()
        {
            IQueryable<Color> colors = _colorRepository.Colors.OrderBy(c => c.Title);

            return PartialView(colors);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return PartialView();
        }

        [HttpPost]
        public IActionResult Create(Color model)
        {
            if (ModelState.IsValid)
            {
                _colorRepository.Create(model);

                IQueryable<Color> colors = _colorRepository.Colors.OrderBy(c => c.Title);
                ViewBag.Success = "created";

                return PartialView("Index", colors);
            }

            return PartialView(model);
        }


        [HttpGet]
        public IActionResult Edit(int colorId)
        {
            Color model = _colorRepository.GetById(colorId);

            return PartialView(model);
        }

        [HttpPost]
        public IActionResult Edit(Color model)
        {
            if (ModelState.IsValid)
            {
                _colorRepository.Update(model);

                IQueryable<Color> colors = _colorRepository.Colors.OrderBy(c => c.Title);
                ViewBag.Success = "edited";

                return PartialView("Index", colors);
            }
                
            return PartialView(model);
        }

        [HttpPost]
        public IActionResult Delete(int colorId)
        {
            Color model = _colorRepository.GetById(colorId);

            if (model != null)
            {
                _colorRepository.Delete(model);
            }

            IQueryable<Color> colors = _colorRepository.Colors.OrderBy(c => c.Title);

            ViewBag.Success = "deleted";

            return PartialView("Index", colors);
        }
    }
}
