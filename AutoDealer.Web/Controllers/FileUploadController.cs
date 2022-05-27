using AutoDealer.Web.ViewModel;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
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
        public async Task<IActionResult> UploadFiles(FilePhotoUpload viewModel)
        {
            if (viewModel.Company == null || viewModel.Model == null || viewModel.Company == string.Empty || viewModel.Model == string.Empty)
            {
                return Problem(statusCode: 403, title: "Не указана компания или модель авто");
            }

            string delimiter = ";";
            string rootFolder = "E:\\Images";

            string rootPath = @$"{rootFolder}\{viewModel.Company}\{viewModel.Model}\";
            string carPhotosFolder = string.Empty;

            DirectoryInfo directory = new DirectoryInfo(rootPath);

            if (Directory.Exists(rootPath))
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
            foreach (var file in viewModel.Files)
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
                        return Problem(statusCode: 403, title: "Ошибка при загрузке файлов.");
                    }
                }
                else
                {
                    return Problem(statusCode: 403, title: $"Фото {file} слишком большое");
                }
            }

            //var input = Content($"<input name=\"Car.Photos\" type =\"hidden\" id=\"Car_Photos\" value=\"{ carPhotoPaths}\" />");
            //var input = Content($"<input name=\"Car.Photos\" type =\"hidden\" id=\"Car_Photos\" value=\"\" />");
            //input.ContentType = "text/html; charset=UTF-8";

            //ViewBag.Success = "Ok";
            //ViewBag.Success = "No";
            return Content(carPhotoPaths);

            //return BadRequest("error message"); //input; // new PartialViewResult { ViewName = "FileUploaded", ViewData = this.ViewData };
        }

        [HttpPost]
        public async Task<IActionResult> Upload(IEnumerable<IFormFile> photoFiles)
        {
            return View();
        }
    }
}
