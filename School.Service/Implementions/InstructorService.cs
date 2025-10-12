using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using School.Data.Entities;
using School.Infrastructure.Abstracts;
using School.Infrastructure.Abstracts.Functions;
using School.Service.Abstracts;

namespace School.Service.Implementions
{
    public class InstructorService : IInstructorService
    {
        private readonly IFileService _fileService;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IInstructorRepository _instructorsRepository;
        private readonly IInstructorFunctionsRepository _instructorFunctionsRepository;

        public InstructorService(IFileService fileService, IHttpContextAccessor httpContextAccessor,
            IInstructorRepository instructorRepository
            , IInstructorFunctionsRepository instructorFunctionsRepository)
        {
            _fileService = fileService;
            _httpContextAccessor = httpContextAccessor;
            _instructorsRepository = instructorRepository;
            _instructorFunctionsRepository = instructorFunctionsRepository;
        }

        public async Task<string> AddInstructorAsync(Instructor instructor, IFormFile file)
        {
            var context = _httpContextAccessor.HttpContext.Request;
            var baseUrl = context.Scheme + "://" + context.Host;
            var imageUrl = await _fileService.UploadImage("Instructors", file);
            if (imageUrl == "FailedToUploadImage")
            {
                return "FailedToUploadImage";
            }
            instructor.Image = baseUrl + imageUrl;
            try
            {
                await _instructorsRepository.AddAsync(instructor);
                return "Success";
            }
            catch (Exception)
            {
                return "FailedInAdd";
            }
        }

        public async Task<decimal> GetSalarySummationOfInstructor()
        {
            decimal result = 0;
            result = _instructorFunctionsRepository.GetSalarySummationOfInstructor("select dbo.GetSalarySummation()");
            return result;
        }

        public async Task<bool> IsNameArExist(string nameAr)
        {
            //Check if the name is Exist Or not
            var student = _instructorsRepository.GetTableNoTracking().Where(x => x.ENameAr.Equals(nameAr)).FirstOrDefault();
            if (student == null) return false;
            return true;
        }

        public async Task<bool> IsNameArExistExcludeSelf(string nameAr, int id)
        {
            //Check if the name is Exist Or not
            var student = await _instructorsRepository.GetTableNoTracking().Where(x => x.ENameAr.Equals(nameAr) & x.InsId != id).FirstOrDefaultAsync();
            if (student == null) return false;
            return true;
        }

        public async Task<bool> IsNameEnExist(string nameEn)
        {
            //Check if the name is Exist Or not
            var student = await _instructorsRepository.GetTableNoTracking().Where(x => x.ENameEn.Equals(nameEn)).FirstOrDefaultAsync();
            if (student == null) return false;
            return true;
        }

        public async Task<bool> IsNameEnExistExcludeSelf(string nameEn, int id)
        {
            //Check if the name is Exist Or not
            var student = await _instructorsRepository.GetTableNoTracking().Where(x => x.ENameEn.Equals(nameEn) & x.InsId != id).FirstOrDefaultAsync();
            if (student == null) return false;
            return true;
        }
    }
}
