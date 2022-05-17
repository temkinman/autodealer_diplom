using AutoDealer.Web.Filters;

namespace AutoDealer.Web.Core.DB.Interfaces
{
    public interface IFilter
    {
        void SaveFilter(CarFilter carFilter);
        CarFilter GetFilter();
        void Reset();
    }
}
