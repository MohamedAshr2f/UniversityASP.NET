using School.Data.Dtos;
using School.Data.Entities.Identity;
using SchoolProject.Data.Results;

namespace School.Service.Abstracts
{
    public interface IAuthorizationService
    {
        public Task<string> AddRoleAsync(string rolename);
        public Task<bool> RoleIsExist(string rolename);
        public Task<string> EditRoleAsync(EditRequestCommand request);
        public Task<string> DeleteRoleAsync(int roleId);
        public Task<List<Role>> GetRolesList();
        public Task<Role> GetRoleById(int roleId);
        public Task<ManageUserRolesResult> ManageUserRolesData(User user);
        public Task<string> UpdateUserRoles(UpdateUserRolesRequest request);
        public Task<ManageUserClaimsResult> ManageUserClaimData(User user);
        public Task<string> UpdateUserClaims(UpdateUserClaimsRequest request);

    }
}
