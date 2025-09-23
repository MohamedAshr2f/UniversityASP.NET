using Microsoft.EntityFrameworkCore;
using School.Data.Entities;
using School.Infrastructure.Abstracts;
using School.Service.Abstracts;

namespace School.Service.Implementions
{
    public class StudentService : IStudentService
    {
        private readonly IStudentRepository _studentRepository;

        public StudentService(IStudentRepository studentRepository)
        {
            _studentRepository = studentRepository;
        }

        public Task<string> AddStudentAsync(Student student)
        {
            throw new NotImplementedException();
        }

        public async Task<Student> GetStudentByIdAsync(int id)
        {
            var stud = await _studentRepository.GetTableNoTracking().Include(s => s.Department)
                 .Include(s => s.StudentSubjects)
                 .ThenInclude(ss => ss.Subjects)
                 .FirstOrDefaultAsync(s => s.StudID == id);
            return stud;

        }

        public async Task<List<Student>> GetStudentsListAsync()
        {
            return await _studentRepository.GetStudentsListAsync();
        }

    }
}
