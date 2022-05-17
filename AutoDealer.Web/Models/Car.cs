using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;

namespace AutoDealer.Web.Models
{
    public class Car
    {
        [Key]
        public int Id { get; set; }

        [Display(Name = "VIN")]
        public string Vin { get; set; }

        [Required(ErrorMessage = "Укажите пробег")]
        [Display(Name = "Пробег")]
        [RegularExpression("^\\d*$", ErrorMessage = "Введите число")]
        public int Kilometre { get; set; }

        public string Photos { get; set; }

        private List<string> _carphotos = new();
        [NotMapped]
        public List<string> CarPhotos
        { 
            get 
            { 
                if(Photos != null)
                    _carphotos = _carphotos?.Count == 0 ? Photos.Split(';', StringSplitOptions.RemoveEmptyEntries).ToList() : _carphotos; 

                return _carphotos;
            }
            set { _carphotos = value; }
        }

        [Display(Name = "Описание")]
        public string Description { get; set; }

        [Column(TypeName = "decimal(8,2)")]
        [Required(ErrorMessage = "Укажите цену")]
        [Display(Name = "Цена")]
        [RegularExpression("^\\d*\\.?\\d*$", ErrorMessage = "Введите число")]
        public decimal Price { get; set; }

        [DisplayFormat(DataFormatString = "{0:dd.MM.yyyy}", ApplyFormatInEditMode = true)]
        [Display(Name = "Дата поступления")]
        public DateTime ArrivalDate { get; set; } = DateTime.Now;

        [Required(ErrorMessage = "Укажите год выпуска")]
        [Display(Name = "Год выпуска")]
        public int ProduceDate { get; set; }

        public virtual Company Company { get; set; }
        public virtual Model Model { get; set; }
        public virtual Color Color { get; set; }
        public virtual AdvSettings Settings { get; set; }
        public virtual Status Status { get; set; }
        public virtual Engine Engine { get; set; }
        public virtual Transmission Transmission { get; set; }
        public virtual CarOwner CarOwner { get; set; }

        public string GetMainPhoto()
        {
            return CarPhotos?.Count > 0 ? CarPhotos[0] : string.Empty;
        }

        public string GetCarImagesFolderPath()
        {
            string subPath = string.Empty;
            string mainPhoto = GetMainPhoto();

            int position = mainPhoto.LastIndexOf('/');

            if (position > -1)
            {
                subPath = mainPhoto.Substring(0, position);
            }

            return subPath;
        }

        public override bool Equals(object obj)
        {
            if (obj == null || !(obj is Car))
            {
                return false;
            }

            return (this.Vin == ((Car)obj).Vin)
                && (this.Kilometre == ((Car)obj).Kilometre)
                && (this.ProduceDate == ((Car)obj).ProduceDate)
                && (this.Company == ((Car)obj).Company)
                && (this.Model == ((Car)obj).Model)
                && (this.Color == ((Car)obj).Color)
                && (this.Settings == ((Car)obj).Settings)
                && (this.Status == ((Car)obj).Status)
                && (this.Engine == ((Car)obj).Engine)
                && (this.Transmission == ((Car)obj).Transmission);
        }
    }
}
