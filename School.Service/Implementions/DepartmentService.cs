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
        public async Task<List<Department>> GetDepartmentListAsync()
        {
            return await _DepartmentRepository.GetDepartmentListAsync();
        }
    }
}
