using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyWebApi.Application.Interfaces
{
    public interface IFileUploadService
    {
        bool DeleteSingleFile(string filePath);
        Task<string> UploadSingleFile(string[] destination, IFormFile file);
        Task<List<string>> UploadMultipleFiles(string[] destination,
        List<IFormFile> files);
    }
}