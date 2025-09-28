using School.Core.Features.Departments.Command.Models;
using School.Data.Entities;

namespace School.Core.Mapping.Departments
{
    public partial class DepartmentProfile
    {
        public void AddDepartmentMapping()
        {
            CreateMap<AddDepartmentCommand, Department>().ForMember(dest => dest.DNameEn, op => op.MapFrom(src => src.DepartmentName));
        }
    }
}
