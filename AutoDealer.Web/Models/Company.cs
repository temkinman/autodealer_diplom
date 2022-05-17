using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AutoDealer.Web.Models
{
    public class Company
    {
        [Key]
        public int Id { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Укажите компанию")]
        [StringLength(30, ErrorMessage = "Имя должно не меньше 2 и не более 30 символов.", MinimumLength = 2)]
        [Display(Name = "Компания")]
        public string Title { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Укажите страну")]
        [StringLength(30, ErrorMessage = "Имя должно не меньше 2 и не более 30 символов.", MinimumLength = 2)]
        [Display(Name = "Страна")]
        public string Country { get; set; }

        public override bool Equals(object obj)
        {
            if (obj == null || !(obj is Company))
            {
                return false;
            }

            return (this.Title == ((Company)obj).Title)
                && (this.Country == ((Company)obj).Country);
        }
    }
}
