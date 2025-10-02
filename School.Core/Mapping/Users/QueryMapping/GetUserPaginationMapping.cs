using School.Core.Features.Users.Query.Results;
using School.Data.Entities.Identity;

namespace School.Core.Mapping.Users
{
    public partial class UserProfile
    {
        public void GetUserPaginationMapping()
        {
            CreateMap<User, GetUserPaginatedResponse>();
        }
    }
}
