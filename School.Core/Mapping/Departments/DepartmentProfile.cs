using AutoMapper;

namespace School.Core.Mapping.Departments
{
    public partial class DepartmentProfile : Profile
    {
        public DepartmentProfile()
        {
            GetDepartmentListMapping();
            GetDepartmentSingleMapping();
            AddDepartmentMapping();
            GetDepartmentStudentCountMapping();
            GetDepartmentStudentCountByIdMapping();
        }
    }
}
