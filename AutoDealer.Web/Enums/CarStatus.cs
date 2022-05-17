using System.ComponentModel.DataAnnotations;

namespace AutoDealer.Web.Enums
{
    public enum CarStatus
    {
        [Display(Name = "Оформляется")]
        InProgress = 1,

        [Display(Name = "Продан")]
        Sold = 2,

        [Display(Name = "Свободен")]
        Opened = 3
    }
}
