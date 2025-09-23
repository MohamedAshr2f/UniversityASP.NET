using Microsoft.EntityFrameworkCore;
using School.Data.Entities;
using School.Infrastructure.Abstracts;
using School.Infrastructure.ApplicationContext;

namespace School.Infrastructure.Repositories
{
    public class StudentRepository : IStudentRepository
    {
        private readonly Context _context;

        public StudentRepository(Context context)
        {
            _context = context;
        }
        public async Task<List<Student>> GetStudentsListAsync()
        {
            var studentList = await _context.Students.ToListAsync();
            return studentList;
        }
    }
}
