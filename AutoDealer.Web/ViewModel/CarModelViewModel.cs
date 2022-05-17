using AutoDealer.Web.Models;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace AutoDealer.Web.ViewModel
{
    public class CarModelViewModel
    {
        public IEnumerable<Company> Companies { get; set; }
        public IEnumerable<Model> Models { get; set; }
        public Model Model { get; set; }
        public int CompanyId { get; set; }

        [Display(Name = "Id")]
        public int ModelId { get; set; }

        [Display(Name = "Названия")]
        public string Title { get; set; }
    }
}
