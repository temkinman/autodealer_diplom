using AutoDealer.Web.Enums;
using AutoDealer.Web.Models;
using System;
using System.Collections;
using System.ComponentModel.DataAnnotations.Schema;

namespace AutoDealer.Web.Filters
{
    [NotMapped]
    public class CarFilter
    {
        public int? KilometreFrom { get; set; }
        public int? KilometreTo { get; set; }
        public decimal? PriceFrom { get; set; }
        public decimal? PriceTo { get; set; }
        public int? ProduceDateFrom { get; set; }
        public int? ProduceDateTo { get; set; }
        public int CompanyId { get; set; } = 0;
        public IEnumerable Companies { get; set; }
        public int ModelId { get; set; } = 0;
        public IEnumerable Models { get; set; }
        public int ColorId { get; set; } = 0;
        public IEnumerable Colors { get; set; }
        public AdvSettings? Settings { get; set; }
        public int EngineTypeId { get; set; } = 0;
        public IEnumerable Engines { get; set; }
        public int TransmissionId { get; set; } = 0;
        public IEnumerable Transmissions { get; set; }

        public bool IsEmpty
        {
            get
            {
                if (KilometreFrom != null) return false;
                if (KilometreTo != null) return false;
                if (PriceFrom != null) return false;
                if (PriceTo != null) return false;
                if (ProduceDateFrom != null) return false;
                if (ProduceDateTo != null) return false;
                if (CompanyId != 0) return false;
                if (ModelId != 0) return false;
                if (ColorId != 0) return false;
                if (EngineTypeId != 0) return false;
                if (TransmissionId != 0) return false;
                if (!Settings.isEmpty) return false;

                return true;
            }
        }
    }
}
