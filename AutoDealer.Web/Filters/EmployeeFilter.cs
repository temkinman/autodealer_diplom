using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace AutoDealer.Web.Filters
{
    [NotMapped]
    public class EmployeeFilter
    {
        public string? FirstName { get; set; }

        public string? LastName { get; set; }

        public DateTime? BDayFrom { get; set; }
        public DateTime? BDayTo { get; set; }
    }
}
