using Microsoft.AspNetCore.Http;

namespace School.Service.Abstracts
{
    public interface IFileService
    {
        public Task<bool> IsValidImageFileMaxLength(IFormFile file);
        public Task<bool> IsValidImageFileExtension(IFormFile file);
        public Task<string> UploadImage(string Location, IFormFile file);
    }
}
