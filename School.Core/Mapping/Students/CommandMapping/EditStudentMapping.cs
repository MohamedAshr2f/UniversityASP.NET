using School.Core.Features.Students.Command.Models;
using School.Data.Entities;

namespace School.Core.Mapping.Students
{
    public partial class StudentProfile
    {
        public void EditStudentMapping()
        {
            CreateMap<EditStudentCommand, Student>().ForMember(dest => dest.StudID, op => op.MapFrom(src => src.Id))
               .ForMember(dest => dest.DID, op => op.MapFrom(src => src.DepartmementId));
        }
    }
}
