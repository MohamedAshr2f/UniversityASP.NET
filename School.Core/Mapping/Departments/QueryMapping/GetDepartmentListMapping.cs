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
                .ForMember(dest => dest.subjectDtos, op => op.MapFrom(src => src.departmentsubjects))
                .ForMember(dest => dest.DepartmentManager, op => op.MapFrom(src => src.Instructor.Localize(src.Instructor.ENameEn, src.Instructor.ENameAr)))
                .ForMember(dest => dest.instructordtos, op => op.MapFrom(src => src.Instructors));

            CreateMap<DepartmentSubject, SubjectDtos>()
                .ForMember(dest => dest.SubjectID, op => op.MapFrom(src => src.SubID))
                 .ForMember(dest => dest.Name, op => op.MapFrom(src => src.Subject.Localize(src.Subject.SubjectNameEn, src.Subject.SubjectNameAr)));


            CreateMap<Student, StudentDtos>().ForMember(dest => dest.StudentName, op => op.MapFrom(src => src.Localize(src.NameEn, src.NameAr)))
                .ForMember(dest => dest.subjectDtos, op => op.MapFrom(src => src.StudentSubject));

            CreateMap<Instructor, InstructorDtos>().ForMember(dest => dest.InstructorName, op => op.MapFrom(src => src.Localize(src.ENameEn, src.ENameAr)));

            CreateMap<StudentSubject, SubjectDtos>()
            .ForMember(dest => dest.SubjectID, op => op.MapFrom(src => src.SubID))
              .ForMember(dest => dest.Name, op => op.MapFrom(src => src.Subject.Localize(src.Subject.SubjectNameEn, src.Subject.SubjectNameAr)));

        }
    }
}
