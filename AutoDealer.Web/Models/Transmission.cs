using AutoDealer.Web.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AutoDealer.Web.Models
{
    public class Transmission
    {
        [Key]
        public int Id { get; set; }
        public TransmissionType Type { get; set; }

        [Display(Name = "Коробка")]
        public string TransmissionType { get; set; }
    }
}
