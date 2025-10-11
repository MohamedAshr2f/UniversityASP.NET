using School.Data.Entities;
using School.Data.Entities.Procedures;
using School.Data.Entities.Views;
using School.Data.Enums;

namespace School.Service.Abstracts
{
    public interface IDepartmentService
    {
        public Task<List<Department>> GetDepartmentListAsync();
        public Task<Department> GetDepartmentByIdAsync(int id);
        public Task<string> AddDepartmentAsync(Department department);
        public Task<bool> IsNameARExist(string name);
        public Task<bool> IsNameENExist(string name);
        public Task<bool> IsDepartmentIdExist(int departmentId);
        public Task<List<ViewDepartment>> GetViewDepartmentDataAsync();
        public IQueryable<Department> GetDepartmentQuerable();
        public IQueryable<Department> FilterDepartmentPaginatedQuerable(string search, DepartmentOrderingEnum orderingEnum);
        public Task<IReadOnlyList<DepartmentStudentCountProc>> GetDepartmentStudentCountProcs(DepartmentStudentCountProcParameters parameters);

    }
}
