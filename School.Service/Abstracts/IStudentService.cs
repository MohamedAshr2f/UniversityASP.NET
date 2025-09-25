using School.Data.Entities;

namespace School.Service.Abstracts
{
    public interface IStudentService
    {
        public Task<List<Student>> GetStudentsListAsync();
        public Task<Student> GetStudentByIdAsync(int id);
        public Task<string> EditStudentAsync(Student student);
        public Task<bool> IsNameExistExcludeSelf(string name, int id);
        public Task<string> AddStudentAsync(Student student);
        public Task<bool> IsNameExist(string name);
    }
}
