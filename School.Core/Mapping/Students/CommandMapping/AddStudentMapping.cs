using School.Core.Features.Students.Command.Models;
using School.Data.Entities;

namespace School.Core.Mapping.Students
{
    public partial class StudentProfile
    {
        public void AddStudentMapping()
        {
            CreateMap<AddStudentCommand, Student>().ForMember(dest => dest.DID, op => op.MapFrom(src => src.DepartmentId));
        }
    }
}
