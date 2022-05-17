using System;
using System.ComponentModel.DataAnnotations;

namespace AutoDealer.Web.ViewModel
{
    public class SaleViewModel
    {
        [Display(Name = "Номер")]
        public int Id { get; set; }

        [Display(Name = "Марка")]
        public string CompanyName { get; set; }

        [Display(Name = "Модель")]
        public string ModelName { get; set; }

        [Display(Name = "Цена")]
        public decimal Price { get; set; }

        [Display(Name = "Дата Продажи")]
        public DateTime Date { get; set; }

        [Display(Name = "Покупатель")]
        public string CustomerFullName { get; set; }

        [Display(Name = "Менеджер")]
        public string EmployeeFullName { get; set; }

    }
}
