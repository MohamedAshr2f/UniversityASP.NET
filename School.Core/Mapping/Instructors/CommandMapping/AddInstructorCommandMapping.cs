using School.Core.Features.Instructors.Command.Models;
using School.Data.Entities;

namespace School.Core.Mapping.Instructors
{
    public partial class InstructorProfile
    {
        public void AddInstructorCommandMapping()
        {
            CreateMap<AddInstructorCommand, Instructor>()
                 .ForMember(dest => dest.Image, opt => opt.Ignore())
                 .ForMember(dest => dest.ENameAr, opt => opt.MapFrom(src => src.NameAr))
                 .ForMember(dest => dest.ENameEn, opt => opt.MapFrom(src => src.NameEn));
        }
    }
}
