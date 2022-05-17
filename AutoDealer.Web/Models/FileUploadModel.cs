using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AutoDealer.Web.Models
{
    [NotMapped]
    public class FileUploadModel
    {
        [DataType(DataType.Upload)]
        [Display(Name = "Выбрать фото")]
        public string File { get; set; }
    }
}
