using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace AutoDealer.Web.Controllers
{
    public class FileUploadController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> UploadFiles(List<IFormFile> photoFile, string company, string model)
        {
            if(company == null || model == null || company == string.Empty || model == string.Empty)
            {
                return BadRequest("Не указано компания или модель авто.");
            }

            string delimiter = ";";
            string rootFolder = "E:\\Images";

            string rootPath = @$"{rootFolder}\{company}\{model}\";
            string carPhotosFolder = string.Empty;

            DirectoryInfo directory = new DirectoryInfo(rootPath);

            if(Directory.Exists(rootPath))
            {
                if (directory.GetDirectories().Length > 0)
                {
                    carPhotosFolder = (Directory.GetDirectories(rootPath))
                                            .OrderByDescending(folder => Directory.GetCreationTime(folder)).FirstOrDefault();

                    carPhotosFolder = new DirectoryInfo(carPhotosFolder).Name;
                }
            }
            else
            {
                Directory.CreateDirectory(rootPath);
            }

            string resultFolder = string.Empty;
            int num;
            bool result = int.TryParse(carPhotosFolder, out num);

            resultFolder = result ? rootPath + (num + 1) : rootPath + 1;
            
            string carPhotoPaths = string.Empty;

            bool firstPhoto = true;
            foreach (var file in photoFile)
            {
                if (file.Length > 0 && file.Length < 6000000)
                {
                    try
                    {
                        var filename = ContentDispositionHeaderValue.Parse(file.ContentDisposition).FileName.Trim('"');
                        Directory.CreateDirectory(resultFolder);

                        string filePath = Path.Combine(resultFolder, file.FileName);

                        carPhotoPaths += firstPhoto ? filePath.Substring(rootFolder.Length).Replace("\\", "/") : file.FileName;
                        carPhotoPaths += delimiter;
                        firstPhoto = false;

                        using (Stream stream = new FileStream(filePath, FileMode.Create))
                        {
                            await file.CopyToAsync(stream);
                        }
                    }
                    catch (Exception)
                    {

                        return BadRequest("Ошибка при загрузке файлов.");
                    }
                }
                else
                {
                    return BadRequest($"Фото {file} слишком большое");
                }
            }

            return Content(carPhotoPaths); // new PartialViewResult { ViewName = "FileUploaded", ViewData = this.ViewData };
        }
    }
}
