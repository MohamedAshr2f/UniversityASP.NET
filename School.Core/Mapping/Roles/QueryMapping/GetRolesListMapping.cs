using School.Core.Features.Authorization.Query.Results;
using School.Data.Entities.Identity;

namespace School.Core.Mapping.Roles
{
    public partial class RoleProfile
    {
        public void GetRolesListMapping()
        {

            CreateMap<Role, GetRoleListResponse>();
        }
    }
}
