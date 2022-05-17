using AutoDealer.Web.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AutoDealer.Web.Models
{
    public class Color
    {
        [Key]
        public int Id { get; set; }

        [NotMapped]
        public ColorType ColorTitle { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Укажите цвет")]
        [StringLength(30, ErrorMessage = "Цвет должен быть не меньше 2 и не более 30 символов.", MinimumLength = 2)]
        [Display(Name = "Цвет")]
        public string Title { get; set; }
    }
}
