using School.Core.Features.Students.Query.Results;
using School.Data.Entities;

namespace School.Core.Mapping.Students
{
    public partial class StudentProfile
    {
        public void GetStudentSingleMapping()
        {
            CreateMap<Student, GetStudentSingleResponse>().ForMember(dest => dest.StudentID, op => op.MapFrom(src => src.StudID))
                .ForMember(dest => dest.StudentName, op => op.MapFrom(src => src.Localize(src.NameEn, src.NameAr)))
                .ForMember(dest => dest.DepartmentName, op => op.MapFrom(src => src.Department.Localize(src.Department.DNameEn, src.Department.DNameAr)))
                 .ForMember(dest => dest.Subjects, op => op.MapFrom(src => src.StudentSubjects));


            CreateMap<StudentSubject, SubjectDto>().ForMember(dest => dest.SubjectID, op => op.MapFrom(src => src.SubID))
                          .ForMember(dest => dest.Name, op => op.MapFrom(src => src.Subjects.Localize(src.Subjects.SubjectNameEn, src.Subjects.SubjectNameAr)));
        }
    }
}
