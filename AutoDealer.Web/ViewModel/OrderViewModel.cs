using AutoDealer.Web.Filters;
using AutoDealer.Web.Models;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace AutoDealer.Web.ViewModel
{
    public class OrderViewModel
    {
        public Car Auto { get; set; }
        public int AutoId { get; set; }
        public Customer Client { get; set; }

        [Required(ErrorMessage = "Укажите цену")]
        [Display(Name = "Финальная цена продажи")]
        [RegularExpression("^\\d*\\.?\\d*$", ErrorMessage = "Введите число")]
        public decimal FinalPrice { get; set; }
    }
}
