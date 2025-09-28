using School.Data.Entities;
using School.Data.Enums;

namespace School.Service.Abstracts
{
    public interface IStudentService
    {
        public Task<List<Student>> GetStudentsListAsync();
        public IQueryable<Student> GetStudentsQuerable();
        public IQueryable<Student> FilterStudentPaginatedQuerable(string search, StudentOrderingEnum orderingEnum);
        public Task<Student> GetStudentByIdAsync(int id);
        public Task<Student> GetStudentByIdwithoutAsync(int id);
        public Task<string> EditStudentAsync(Student student);
        public Task<bool> IsNameExistExcludeSelf(string name, int id);
        public Task<string> AddStudentAsync(Student student);
        public Task<bool> IsNameARExist(string name);
        public Task<bool> IsNameENExist(string name);
        public Task<string> DeleteStudentAsync(Student student);
    }
}
