using System.ComponentModel.DataAnnotations;

namespace AutoDealer.Web.Enums
{
    public enum TransmissionType
    {
        [Display(Name = "Автомат")]
        Automatic,

        [Display(Name = "Механика")]
        Manual
    }
}
