using AutoDealer.Web.ViewModel;
using System;
using System.ComponentModel.DataAnnotations;

namespace AutoDealer.Web.Models
{
    public class CarOwner
    {
        [Key]
        public int Id { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Укажите имя")]
        [StringLength(30, ErrorMessage = "Имя должно не меньше 2 и не более 30 символов.", MinimumLength = 2)]
        [Display(Name = "Имя")]
        public string FirstName { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Укажите фамилию")]
        [StringLength(30, ErrorMessage = "Имя должно не меньше 2 и не более 30 символов.", MinimumLength = 2)]
        [Display(Name = "Фамилия")]
        public string LastName { get; set; }

        [Required(ErrorMessage = "Укажите дату рождения")]
        [DisplayFormat(DataFormatString = "{0:dd.MM.yyyy}", ApplyFormatInEditMode = true)]
        [Display(Name = "Дата рождения")]
        public DateTime BDay { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Укажите серию паспорта")]
        [Display(Name = "Паспорт")]
        public string PassportSeries { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Укажите телефон")]
        [Display(Name = "Телефон")]
        public string Phone { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Укажите город")]
        [Display(Name = "Город")]
        public string City { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Укажите улицу")]
        [Display(Name = "Улица")]
        public string Street { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Укажите номер дома")]
        [Display(Name = "Дом")]
        public string House { get; set; }

        [Display(Name = "Квартира")]
        public int? Flat { get; set; }
        public string FullName
        {
            get => FirstName + " " + LastName;
        }

        public override bool Equals(object obj)
        {
            if (obj == null || !(obj is CarOwner))
            {
                return false;
            }

            return (this.FirstName == ((CarOwner)obj).FirstName)
                && (this.LastName == ((CarOwner)obj).LastName)
                && (this.Phone == ((CarOwner)obj).Phone)
                && (this.BDay == ((CarOwner)obj).BDay)
                && (this.PassportSeries == ((CarOwner)obj).PassportSeries)
                && (this.City == ((CarOwner)obj).City)
                && (this.Street == ((CarOwner)obj).Street)
                && (this.House == ((CarOwner)obj).House);
        }
    }
}
