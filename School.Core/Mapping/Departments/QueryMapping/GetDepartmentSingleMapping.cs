using School.Core.Features.Departments.Query.Results;
using School.Data.Entities;

namespace School.Core.Mapping.Departments
{
    public partial class DepartmentProfile
    {
        public void GetDepartmentSingleMapping()
        {
            CreateMap<Department, GetDepartmentSingleResponse>().ForMember(dest => dest.DepartmentName, op => op.MapFrom(src => src.Localize(src.DNameEn, src.DNameAr)))
               .ForMember(dest => dest.StudentDtos, op => op.MapFrom(src => src.Students))
               .ForMember(dest => dest.SubjectDtos, op => op.MapFrom(src => src.departmentsubjects))
                .ForMember(dest => dest.DepartmentManager, op => op.MapFrom(src => src.Instructor.Localize(src.Instructor.ENameEn, src.Instructor.ENameAr)))
                .ForMember(dest => dest.instructordtos, op => op.MapFrom(src => src.Instructors));

            CreateMap<DepartmentSubject, SubjectDtos>()
              .ForMember(dest => dest.SubjectID, op => op.MapFrom(src => src.SubID))
              .ForMember(dest => dest.Name, op => op.MapFrom(src => src.Subject.Localize(src.Subject.SubjectNameEn, src.Subject.SubjectNameAr)));

            CreateMap<Instructor, InstructorDtos>().ForMember(dest => dest.InstructorName, op => op.MapFrom(src => src.Localize(src.ENameEn, src.ENameAr)));

            CreateMap<Student, StudentDtos>()
                .ForMember(dest => dest.StudentName, op => op.MapFrom(src => src.Localize(src.NameEn, src.NameAr)))
                .ForMember(dest => dest.subjectDtos, op => op.MapFrom(src => src.StudentSubject));

            CreateMap<StudentSubject, SubjectDtos>()
                .ForMember(dest => dest.SubjectID, op => op.MapFrom(src => src.SubID))
                .ForMember(dest => dest.Name, op => op.MapFrom(src => src.Subject.Localize(src.Subject.SubjectNameEn, src.Subject.SubjectNameAr)));



        }
    }
}
