using Microsoft.EntityFrameworkCore;
using School.Data.Entities;
using School.Infrastructure.Abstracts;
using School.Infrastructure.ApplicationContext;
using School.Infrastructure.InfrastructureBases;

namespace School.Infrastructure.Repositories
{
    public class DepartmentRepository : GenericRepositoryAsync<Department>, IDepartmentRepository
    {
        private readonly DbSet<Department> _departments;

        public DepartmentRepository(Context context) : base(context)
        {
            _departments = context.Set<Department>();
        }
        public async Task<List<Department>> GetDepartmentListAsync()
        {
            return await _departments.Include(d => d.Students)
                .ThenInclude(s => s.StudentSubjects)
                .ThenInclude(ss => ss.Subjects)
                .Include(d => d.DepartmentSubjects)
                .ThenInclude(ds => ds.Subjects).AsNoTracking().ToListAsync();
        }
    }
}
