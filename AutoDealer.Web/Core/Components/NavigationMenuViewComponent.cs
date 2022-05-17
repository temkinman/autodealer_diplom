using Microsoft.AspNetCore.Mvc;

namespace AutoDealer.Web.Core.Components
{
    public class NavigationMenuViewComponent : ViewComponent
    {
        private static string[] _menu = { "авто", "продажи", "приемка авто", "о нас", "админка" };

		public IViewComponentResult Invoke()
		{
			ViewBag.SelectedCategory = RouteData?.Values["category"]; //тут ложим category в словарь чтобы во вьюхе NavigationMenu
																	  // сравнивать ViewBag.SelectedCategory выбранная это категория или нет и применять к ней класс

			//IOrderedQueryable<string> categories = _repository.Products
			//	.Select(product => product.Category)
			//	.Distinct()
			//	.OrderBy(category => category);

			return View(_menu);
		}
	}
}
