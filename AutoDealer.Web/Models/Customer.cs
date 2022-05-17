using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AutoDealer.Web.Models
{
    public class Customer
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

        [Required(ErrorMessage = "Укажите электронную почту")]
        [RegularExpression("^([\\w\\.\\-]+)@([\\w\\-]+)((\\.(\\w){2,3})+)$", ErrorMessage = "Не корректный е-мэйл")]
        [Display(Name = "Е-мэйл")]
        public string Email { get; set; }

        public string FullName
        {
            get => FirstName + " " + LastName;
        }

        public override bool Equals(object obj)
        {
            if (obj == null || !(obj is Customer))
            {
                return false;
            }

            return (this.FirstName == ((Customer)obj).FirstName)
                && (this.LastName == ((Customer)obj).LastName)
                && (this.Phone == ((Customer)obj).Phone)
                && (this.BDay == ((Customer)obj).BDay)
                && (this.PassportSeries == ((Customer)obj).PassportSeries)
                && (this.City == ((Customer)obj).City)
                && (this.Street == ((Customer)obj).Street)
                && (this.House == ((Customer)obj).House)
                && (this.Email == ((Customer)obj).Email);
        }
    }
}
