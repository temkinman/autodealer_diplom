using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AutoDealer.Web.Models
{
    public class Model
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Укажите модель")]
        [Display(Name = "Модель")]
        public string Title { get; set; }

        [ValidateNever]
        public virtual Company Company { get; set; }
    }
}
