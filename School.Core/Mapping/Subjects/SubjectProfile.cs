using AutoMapper;

namespace School.Core.Mapping.Subjects
{
    public partial class SubjectProfile : Profile
    {
        public SubjectProfile()
        {
            GetSubjectListMapping();
        }
    }
}
