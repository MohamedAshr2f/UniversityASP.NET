using School.Core.Features.Departments.Query.Results;
using School.Data.Entities.Views;

namespace School.Core.Mapping.Departments
{
    public partial class DepartmentProfile
    {
        public void GetDepartmentStudentCountMapping()
        {
            CreateMap<ViewDepartment, GetDepartmentStudentListCountResponse>()
                 .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Localize(src.DNameEn, src.DNameAr)))
                 .ForMember(dest => dest.StudentCount, opt => opt.MapFrom(src => src.StudentCount));
        }
    }
}
