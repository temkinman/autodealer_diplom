using AutoDealer.Web.Filters;
using AutoDealer.Web.Models;
using System.Collections.Generic;

namespace AutoDealer.Web.ViewModel
{
    public class CarsListViewModel
    {
        public IEnumerable<Car> Cars { get; set; }
        public PagingInfo PagingInfo { get; set; }
        public CarFilter CarFilter { get; set; }
        public SortViewModel SortViewModel { get; set; }
        public string SelectedCompany { get; set; }
        public string SelectedModel { get; set; }
    }
}
