using School.Core.Features.Departments.Query.Results;
using School.Data.Entities;

namespace School.Core.Mapping.Departments
{
    public partial class DepartmentProfile
    {
        public void GetDepartmentListMapping()
        {
            CreateMap<Department, GetDepartmentListResponse>().ForMember(dest => dest.DepartmentID, op => op.MapFrom(src => src.DID))
                .ForMember(dest => dest.DepartmentName, op => op.MapFrom(src => src.Localize(src.DNameEn, src.DNameAr)))
                .ForMember(dest => dest.students, op => op.MapFrom(src => src.Students))
                .ForMember(dest => dest.subjectDtos, op => op.MapFrom(src => src.DepartmentSubjects));

            CreateMap<DepartmentSubject, SubjectDtos>()
                .ForMember(dest => dest.SubjectID, op => op.MapFrom(src => src.SubID))
                 .ForMember(dest => dest.Name, op => op.MapFrom(src => src.Subjects.Localize(src.Subjects.SubjectNameEn, src.Subjects.SubjectNameAr)));


            CreateMap<Student, StudentDtos>().ForMember(dest => dest.StudentName, op => op.MapFrom(src => src.Localize(src.NameEn, src.NameAr)))
                .ForMember(dest => dest.subjectDtos, op => op.MapFrom(src => src.StudentSubjects));

            CreateMap<StudentSubject, SubjectDtos>()
               .ForMember(dest => dest.SubjectID, op => op.MapFrom(src => src.SubID))
               .ForMember(dest => dest.Name, op => op.MapFrom(src => src.Subjects.Localize(src.Subjects.SubjectNameEn, src.Subjects.SubjectNameAr)));

        }
    }
}
