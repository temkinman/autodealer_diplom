using System.ComponentModel.DataAnnotations;

namespace AutoDealer.Web.Models
{
    public class EngineType
    {
        [Key]
        public int Id { get; set; }

        [Display(Name = "Двигатель")]
        public string Title { get; set; }
    }
}
