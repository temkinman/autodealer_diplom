using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AutoDealer.Web.Models
{
    public class Engine
    {
        [Key]
        public int Id { get; set; }

        [Display(Name = "Тип двигателя")]
        public virtual EngineType Type { get; set; }

        [Column(TypeName = "float(3,2)")]
        [Display(Name = "Объем двигателя")]
        public float Capacity { get; set; }
    }
}
