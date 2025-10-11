using School.Core.Features.Departments.Query.Models;
using School.Core.Features.Departments.Query.Results;
using School.Data.Entities.Procedures;

namespace School.Core.Mapping.Departments
{
    public partial class DepartmentProfile
    {
        public void GetDepartmentStudentCountByIdMapping()
        {
            CreateMap<GetDepartmentStudentCountByIDQuery, DepartmentStudentCountProcParameters>();

            CreateMap<DepartmentStudentCountProc, GetDepartmentStudentCountByIDResponse>()
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Localize(src.DNameEn, src.DNameAr)))
                .ForMember(dest => dest.StudentCount, opt => opt.MapFrom(src => src.StudentCount));

        }
    }
}
