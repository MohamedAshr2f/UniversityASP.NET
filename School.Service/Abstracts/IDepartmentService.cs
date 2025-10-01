using School.Data.Entities;
using School.Data.Enums;

namespace School.Service.Abstracts
{
    public interface IDepartmentService
    {
        public Task<List<Department>> GetDepartmentListAsync();
        public Task<Department> GetDepartmentByIdAsync(int id);
        public Task<string> AddDepartmentAsync(Department department);

        public Task<bool> IsDepartmentIdExist(int departmentId);
        public IQueryable<Department> GetDepartmentQuerable();
        public IQueryable<Department> FilterDepartmentPaginatedQuerable(string search, DepartmentOrderingEnum orderingEnum);

    }
}
