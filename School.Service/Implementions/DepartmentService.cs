using Microsoft.EntityFrameworkCore;
using School.Data.Entities;
using School.Infrastructure.Abstracts;
using School.Service.Abstracts;

namespace School.Service.Implementions
{
    public class DepartmentService : IDepartmentService
    {
        private readonly IDepartmentRepository _DepartmentRepository;
        public DepartmentService(IDepartmentRepository DepartmentRepository)
        {
            _DepartmentRepository = DepartmentRepository;
        }

        public async Task<string> AddDepartmentAsync(Department department)
        {
            await _DepartmentRepository.AddAsync(department);
            return "successful";
        }

        public async Task<Department> GetDepartmentByIdAsync(int id)
        {
            var Dep = await _DepartmentRepository.GetTableNoTracking().Include(d => d.Students)
                .ThenInclude(s => s.StudentSubject)
                .ThenInclude(ss => ss.Subject)
                .Include(d => d.departmentsubjects)
                .ThenInclude(ds => ds.Subject)
                 .Include(d => d.Instructor)
                 .Include(d => d.Instructors).FirstOrDefaultAsync(d => d.DID == id);
            return Dep;
        }

        public async Task<List<Department>> GetDepartmentListAsync()
        {
            // return await _DepartmentRepository.GetDepartmentListAsync();
            var departments = await _DepartmentRepository.GetTableNoTracking().Include(d => d.Students)
                 .ThenInclude(s => s.StudentSubject)
                 .ThenInclude(ss => ss.Subject)
                 .Include(d => d.departmentsubjects)
                 .ThenInclude(ds => ds.Subject)
                 .Include(d => d.Instructor)
                 .Include(d => d.Instructors).ToListAsync();
            return departments;
        }
    }
}
