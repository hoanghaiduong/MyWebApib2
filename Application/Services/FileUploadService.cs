using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MyWebApi.Application.Interfaces;

namespace MyWebApi.Application.Services
{
    public class FileUploadService : IFileUploadService
    {
        private readonly IWebHostEnvironment _environment;

        public FileUploadService(IWebHostEnvironment environment)
        {
            _environment = environment;
        }

        public bool DeleteSingleFile(string filePath)
        {
            // /uploads/users/img1.jpg

            var fullPath = Path.Combine(_environment.WebRootPath, filePath);
            //wwwroot/uploads/users/img1.jpg
            if (File.Exists(fullPath))
            {
                File.Delete(fullPath);
                return true;
            }
            return false;
        }

        public async Task<List<string>> UploadMultipleFiles(string[] destination, List<IFormFile> files)
        {
            /*
                [
                    "img1.jpg",
                    "img2.png"
                ]
            
            */
            var filePaths = new List<string>();
            foreach (var file in files)
            {
                var filePath = await UploadSingleFile(destination, file);
                if (filePath != null)
                {
                    filePaths.Add(filePath);
                }
            }
            return filePaths;

        }

        public async Task<string> UploadSingleFile(string[] destination, IFormFile file)
        {
            if (file == null || file.Length == 0)
                return null;

            var fileName = $"{Guid.NewGuid()}_{file.FileName}";//đặt tên file
            var uploadPath = Path.Combine(_environment.WebRootPath,
            Path.Combine(destination));// kết hợp root directory
            //root/users/
            if (!Directory.Exists(uploadPath))
                Directory.CreateDirectory(uploadPath);
            //root/users/img2.jpg
            var filePath = Path.Combine(uploadPath, fileName);////root/users/img2.jpg
            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }
            var relativePath = Path.Combine(destination).Replace("\\", "/");
            return $"{relativePath}/{fileName}";
        }
    }
}