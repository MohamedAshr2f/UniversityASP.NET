using School.Core.Features.Subjects.Query.Results;
using School.Data.Entities;

namespace School.Core.Mapping.Subjects
{
    public partial class SubjectProfile
    {
        public void GetSubjectSingleMapping()
        {
            CreateMap<Subject, GetSubjectSingleResponse>().ForMember(dest => dest.SubjectID, op => op.MapFrom(src => src.SubID))
                 .ForMember(dest => dest.Name, op => op.MapFrom(src => src.SubjectName))
                 .ForMember(dest => dest.Department, op => op.MapFrom(src => src.DepartmetsSubjects));

            CreateMap<DepartmentSubject, DepartmentDto>().ForMember(dest => dest.DepartmentName, op => op.MapFrom(src => src.Department.DName));
        }
    }
}
