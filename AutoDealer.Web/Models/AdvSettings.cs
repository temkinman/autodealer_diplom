using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace AutoDealer.Web.Models
{
    public class AdvSettings
    {
        [Key]
        public int Id { get; set; }

        [Display(Name = "ABS")]
        public bool Abs { get; set; } = false;

        [Display(Name = "ESP")]
        public bool Esp { get; set; } = false;

        [Display(Name = "Парктроники")]
        public bool ParkSensors { get; set; } = false;

        [Display(Name = "Камера")]
        public bool Camera { get; set; } = false;

        [Display(Name = "Круиз")]
        public bool Cruiz { get; set; } = false;

        [Display(Name = "Кондиционер")]
        public bool AirCondition { get; set; } = false;

        [Display(Name = "Климат")]
        public bool ClimatControl { get; set; } = false;

        [Display(Name = "Навигация")]
        public bool Navigation { get; set; } = false;

        [Display(Name = "Bluetooth")]
        public bool Bluetooth { get; set; } = false;

        public bool isEmpty { get 
            {
                if(
                    Abs == false &&
                    Esp == false &&
                    ParkSensors == false &&
                    Camera == false &&
                    Cruiz == false &&
                    AirCondition == false &&
                    ClimatControl == false &&
                    Navigation == false &&
                    Bluetooth == false
                    )
                    return true;

                return false;
            } 
        }

        public override bool Equals(object obj)
        {
            if (obj == null || !(obj is AdvSettings))
            {
                return false;
            }

            return (this.Abs == ((AdvSettings)obj).Abs)
                && (this.Esp == ((AdvSettings)obj).Esp)
                && (this.ParkSensors == ((AdvSettings)obj).ParkSensors)
                && (this.Camera == ((AdvSettings)obj).Camera)
                && (this.Cruiz == ((AdvSettings)obj).Cruiz)
                && (this.AirCondition == ((AdvSettings)obj).AirCondition)
                && (this.ClimatControl == ((AdvSettings)obj).ClimatControl)
                && (this.Navigation == ((AdvSettings)obj).Navigation)
                && (this.Bluetooth == ((AdvSettings)obj).Bluetooth);
        }

        public override string ToString()
        {
            StringBuilder result = new StringBuilder();
            if (Abs) result.Append("abs, ");
            if (Esp) result.Append("esp, ");
            if (ParkSensors) result.Append("датчики парковки, ");
            if (Camera) result.Append("камера, ");
            if (Cruiz) result.Append("круиз-контроль, ");
            if (AirCondition) result.Append("кондиционер, ");
            if (ClimatControl) result.Append("климат-контроль, ");
            if (Navigation) result.Append("навигация, ");
            if (Bluetooth) result.Append("bluetooth, ");
            
            string strSettings = result.ToString().Trim();

            return strSettings.Length > 0 ? strSettings.Substring(0, strSettings.Length - 1) : strSettings;
        }
    }
}
