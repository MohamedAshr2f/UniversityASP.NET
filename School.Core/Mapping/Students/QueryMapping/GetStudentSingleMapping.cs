using School.Core.Features.Students.Query.Results;
using School.Data.Entities;

namespace School.Core.Mapping.Students
{
    public partial class StudentProfile
    {
        public void GetStudentSingleMapping()
        {
            CreateMap<Student, GetStudentSingleResponse>().ForMember(dest => dest.StudentID, op => op.MapFrom(src => src.StudID))
                .ForMember(dest => dest.StudentName, op => op.MapFrom(src => src.Name))
                .ForMember(dest => dest.DepartmentName, op => op.MapFrom(src => src.Department.DName))
                 .ForMember(dest => dest.Subjects, op => op.MapFrom(src => src.StudentSubjects));


            CreateMap<StudentSubject, SubjectDto>().ForMember(dest => dest.SubjectID, op => op.MapFrom(src => src.StudSubID))
                          .ForMember(dest => dest.Name, op => op.MapFrom(src => src.Subjects.SubjectName));
        }
    }
}
