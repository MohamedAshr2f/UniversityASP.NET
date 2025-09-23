using Microsoft.EntityFrameworkCore;
using School.Data.Entities;
using School.Infrastructure.Abstracts;
using School.Infrastructure.ApplicationContext;

namespace School.Infrastructure.Repositories
{
    public class DepartmentRepository : IDepartmentRepository
    {
        private readonly Context _context;

        public DepartmentRepository(Context context)
        {
            _context = context;
        }
        public async Task<List<Department>> GetDepartmentListAsync()
        {
            return await _context.Departments.Include(d => d.Students)
                .ThenInclude(s => s.StudentSubjects)
                .ThenInclude(ss => ss.Subjects)
                .Include(d => d.DepartmentSubjects)
                .ThenInclude(ds => ds.Subjects).AsNoTracking().ToListAsync();
        }
    }
}
