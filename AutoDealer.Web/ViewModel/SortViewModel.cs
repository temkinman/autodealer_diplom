using AutoDealer.Web.Enums;

namespace AutoDealer.Web.ViewModel
{
    public class SortViewModel
    {
        public SortState ModelSort { get; private set; }
        public SortState PriceSort { get; private set; }
        public SortState YearSort { get; private set; }
        public SortState KilometreSort { get; private set; }
        public SortState DateSort { get; private set; }
        public SortState CompanySort { get; private set; }
        public SortState CustomerSort { get; private set; }
        public SortState Current { get; private set; }
        
        public SortViewModel(SortState sortOrder)
        {
            ModelSort = sortOrder == SortState.ModelAsc ? SortState.ModelDesc : SortState.ModelAsc;
            PriceSort = sortOrder == SortState.PriceAsc ? SortState.PriceDesc : SortState.PriceAsc;
            YearSort = sortOrder == SortState.YearAsc ? SortState.YearDesc : SortState.YearAsc;
            KilometreSort = sortOrder == SortState.KilometreAsc ? SortState.KilometreDesc : SortState.KilometreAsc;
            DateSort = sortOrder == SortState.DateAsc ? SortState.DateDesc : SortState.DateAsc;
            CompanySort = sortOrder == SortState.CompanyAsc ? SortState.CompanyDesc : SortState.CompanyAsc;
            CustomerSort = sortOrder == SortState.CustomerAsc ? SortState.CustomerDesc : SortState.CustomerAsc;
            Current = sortOrder;
        }
    }
}
