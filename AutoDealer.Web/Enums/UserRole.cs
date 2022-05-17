using System.ComponentModel.DataAnnotations;

namespace AutoDealer.Web.Enums
{
    public enum ManagerRole
    {
        [Display(Name = "Администратор")]
        Admin,

        [Display(Name = "Менеджер")]
        Manager,

        [Display(Name = "Старший менеджер")]
        SuperManager,

        [Display(Name = "Директор")]
        Director
    }
}
