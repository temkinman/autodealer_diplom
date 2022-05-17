using AutoDealer.Web.Models;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace AutoDealer.Web.Filters
{
    [NotMapped]
    public class SaleFilter
    {
        public Employee Employee { get; set; }
        public DateTime? SaleDateFrom { get; set; }
        public DateTime? SaleDateTo { get; set; }
        public decimal? PriceFrom { get; set; }
        public decimal? PriceTo { get; set; }
        public Customer Customer { get; set; }
        public Car Car { get; set; }
    }
}
