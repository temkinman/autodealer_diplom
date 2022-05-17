using AutoDealer.Web.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AutoDealer.Web.Models
{
    public class Status
    {
        [Key]
        public int Id { get; set; }
        public string StatusName { get; set; }
    }
}
