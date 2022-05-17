using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AutoDealer.Web.Models
{
    [NotMapped]
    public class SaleReport
    {
        public int Id { get; set; }
        public DateTime? DateFrom { get; set; }
        public DateTime? DateTo { get; set; }
        public List<Sale> Sales { get; set; }
        public int CarsCount { get; set; } = 0;
        public int TotalSumm { get; set; } = 0;
    }
}
