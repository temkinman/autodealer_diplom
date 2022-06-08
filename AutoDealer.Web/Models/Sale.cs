using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AutoDealer.Web.Models
{
    public class Sale
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Укажите цену")]
        [Display(Name = "Цена")]
        [Column(TypeName = "decimal(8,0)")]
        public decimal FinalPrice { get; set; }

        [Required(ErrorMessage = "Укажите дату продажи")]
        [DisplayFormat(DataFormatString = "{0:dd.MM.yyyy}", ApplyFormatInEditMode = true)]
        [Display(Name = "Дата")]
        public DateTime SaledDate { get; set; } = DateTime.Now;

        public virtual Car Car { get; set; }
        public virtual Customer Customer { get; set; }
        public virtual Employee Employee { get; set; }
    }
}
