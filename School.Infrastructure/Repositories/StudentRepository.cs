using Microsoft.EntityFrameworkCore;
using School.Data.Entities;
using School.Infrastructure.Abstracts;
using School.Infrastructure.ApplicationContext;
using School.Infrastructure.InfrastructureBases;

namespace School.Infrastructure.Repositories
{
    public class StudentRepository : GenericRepositoryAsync<Student>, IStudentRepository
    {
        private readonly DbSet<Student> _students;

        public StudentRepository(Context context) : base(context)
        {
            _students = context.Set<Student>();
        }

        public async Task<List<Student>> GetStudentsListAsync()
        {
            var studentList = await _students.Include(s => s.Department).Include(s => s.StudentSubjects).ThenInclude(ss => ss.Subjects).ToListAsync();
            return studentList;
        }
    }
}
