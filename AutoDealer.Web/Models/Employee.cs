using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AutoDealer.Web.Models
{
    public class Employee
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

        [Display(Name = "Телефон")]
        public string Phone { get; set; }

        [Required(ErrorMessage = "Укажите электронную почту")]
        [RegularExpression("^([\\w\\.\\-]+)@([\\w\\-]+)((\\.(\\w){2,3})+)$", ErrorMessage = "Не корректный е-мэйл")]
        [Display(Name = "Е-мэйл")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Заполните поле пароль")]
        [DataType(DataType.Password)]
        [Display(Name = "Пароль")]
        public string Password { get; set; }

        [Required(ErrorMessage = "Укажите дату первого рабочего дня")]
        [DisplayFormat(DataFormatString = "{0:dd.MM.yyyy}", ApplyFormatInEditMode = true)]
        [Display(Name = "Дата начало работы")]
        public DateTime StartWorkDay { get; set; } = DateTime.Now;

        [ValidateNever]
        public virtual Role Role { get; set; }
        public string FullName { 
            get => FirstName + " " + LastName;
        }
    }
}
