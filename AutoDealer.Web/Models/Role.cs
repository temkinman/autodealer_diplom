using AutoDealer.Web.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AutoDealer.Web.Models
{
    public class Role
    {
        [Key]
        public int Id { get; set; }

        [NotMapped]
        public ManagerRole UserRole { get; set; }

        [Required(ErrorMessage = "Укажите должность")]
        [Display(Name = "Должность")]
        public string AccessRole { get; set; }
    }
}
