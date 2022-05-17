using AutoDealer.Web.Filters;
using AutoDealer.Web.Models;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace AutoDealer.Web.ViewModel
{
    public class AddingUsedCarViewModel
    {
        public Car Auto { get; set; } = new();
        public int AutoId { get; set; } = 0;
        public int CarOwnerId { get; set; } = 0;
        public CarOwner CarOwner { get; set; } = new();
        public string Owner { get; set; }
        public int CompanyId { get; set; } = 0;
        public IEnumerable? Companies { get; set; }
        public int ModelId { get; set; } = 0;
        public IEnumerable? Models { get; set; }
        public int ColorId { get; set; } = 0;
        public IEnumerable? Colors { get; set; }
        public int EngineTypeId { get; set; } = 0;
        public IEnumerable? EngineTypes { get; set; }
        public int TransmissionId { get; set; } = 0;
        public IEnumerable? Transmissions { get; set; }
    }
}
