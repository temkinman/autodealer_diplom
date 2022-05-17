using System.ComponentModel.DataAnnotations;

namespace AutoDealer.Web.Enums
{
    public enum ColorType
    {
        [Display(Name = "Бежевый")]
        Beige,

        [Display(Name = "Коричневый")]
        Brown,

        [Display(Name = "Зеленый")]
        Green,

        [Display(Name = "Красный")]
        Red,

        [Display(Name = "Серебристый")]
        Silver,

        [Display(Name = "Белый")]
        White,

        [Display(Name = "Синий")]
        Blue,

        [Display(Name = "Желтый")]
        Yellow,

        [Display(Name = "Серый")]
        Grey,

        [Display(Name = "Оранжевый")]
        Orange,

        [Display(Name = "Черный")]
        Black,

        [Display(Name = "металлик")]
        Metallic
    }
}
