using AutoDealer.Web.Core.DB.Interfaces;
using AutoDealer.Web.Filters;

namespace AutoDealer.Web.Core.Infrastructure
{
    public class Filter : IFilter
    {
        private CarFilter _filter = null;
        public CarFilter GetFilter()
        {
            return _filter;
        }

        public void Reset()
        {
            _filter = null;
        }

        public void SaveFilter(CarFilter carFilter)
        {
            _filter = carFilter;
        }
    }
}
