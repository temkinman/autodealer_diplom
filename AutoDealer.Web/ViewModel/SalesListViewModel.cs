using AutoDealer.Web.Filters;
using AutoDealer.Web.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace AutoDealer.Web.ViewModel
{
    public class SalesListViewModel : BaseListViewModel
    {
        public IEnumerable<SaleViewModel> Sales { get; set; }
        //public FilterViewModel FilterViewModel { get; set; }
        public string SelectedCompany { get; set; }
        public string SelectedModel { get; set; }
        public DateTime? DateFrom { get; set; }
        public DateTime? DateTo { get; set; }

        [Display(Name = "Марка")]
        public string CompanyTitle { get; set; }

        [Display(Name = "Модель")]
        public string ModelTitle { get; set; }

        [Display(Name = "Цена")]
        public decimal Price { get; set; }

        [Display(Name = "Дата Продажи")]
        public DateTime Date { get; set; }

        [Display(Name = "Покупатель")]
        public string Customer { get; set; }

        [Display(Name = "Менеджер")]
        public string Employee { get; set; }

        [Display(Name = "Количество автомобилей")]
        public int TotalCars { get; set; }

        [Display(Name = "Сумма")]
        public decimal TotalSum { get; set; }
    }
}
