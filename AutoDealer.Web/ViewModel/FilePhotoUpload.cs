using Microsoft.AspNetCore.Http;
using System.Collections.Generic;

namespace AutoDealer.Web.ViewModel
{
    public class FilePhotoUpload
    {
        public IEnumerable<IFormFile> Files { get; set; }

        public string Company { get; set; }
        public string Model { get; set; }
    }
}
