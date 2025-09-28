using School.Core.Features.Subjects.Query.Results;
using School.Data.Entities;

namespace School.Core.Mapping.Subjects
{
    public partial class SubjectProfile
    {
        public void GetSubjectListMapping()
        {
            CreateMap<Subject, GetSubjectsListResponse>().ForMember(dest => dest.SubjectID, op => op.MapFrom(src => src.SubID))
                 .ForMember(dest => dest.Name, op => op.MapFrom(src => src.Localize(src.SubjectNameEn, src.SubjectNameAr)))
                 .ForMember(dest => dest.Department, op => op.MapFrom(src => src.DepartmetsSubjects));

            CreateMap<DepartmentSubject, DepartmentDto>().ForMember(dest => dest.DepartmentName, op => op.MapFrom(src => src.Department.Localize(src.Department.DNameEn, src.Department.DNameAr)));
        }
    }
}
