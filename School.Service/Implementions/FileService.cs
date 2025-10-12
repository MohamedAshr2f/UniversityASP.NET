using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using School.Data.Helpers;
using School.Service.Abstracts;

namespace School.Service.Implementions
{
    public class FileService : IFileService
    {
        private readonly IWebHostEnvironment _webHostEnvironment;
        public FileService(IWebHostEnvironment webHostEnvironment)
        {
            _webHostEnvironment = webHostEnvironment;
        }

        public async Task<bool> IsValidImageFileExtension(IFormFile file)
        {
            if (file is not null)
            {
                var extension = Path.GetExtension(file.FileName);
                var isAllowed = FileSettings.AllowedExtensions.Split(',').Contains(extension, StringComparer.OrdinalIgnoreCase);
                if (!isAllowed)
                {
                    return false;
                }
            }
            return true;

        }

        public async Task<bool> IsValidImageFileMaxLength(IFormFile file)
        {

            if (file is not null)
            {
                if (file.Length > FileSettings.MaxFileSizeInBytes)
                {
                    return false;
                }
            }
            return true;
        }

        public async Task<string> UploadImage(string Location, IFormFile file)
        {
            var path = _webHostEnvironment.WebRootPath + "/" + Location + "/";
            var extension = Path.GetExtension(file.FileName);
            var fileName = Guid.NewGuid().ToString().Replace("-", string.Empty) + extension;
            var fullPath = Path.Combine(path, fileName);
            try
            {
                if (!Directory.Exists(path))
                {
                    Directory.CreateDirectory(path);
                }
                using (FileStream filestreem = File.Create(fullPath))
                {
                    await file.CopyToAsync(filestreem);
                    await filestreem.FlushAsync();
                    return $"/{Location}/{fileName}";
                }
            }
            catch (Exception)
            {
                return "FailedToUploadImage";
            }

        }
    }
}
