using School.Core.Features.Users.Command.Models;
using School.Data.Entities.Identity;

namespace School.Core.Mapping.Users
{
    public partial class UserProfile
    {
        public void AddUserMapping()
        {
            CreateMap<AddUserCommand, User>();
        }
    }
}
