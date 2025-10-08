using School.Data.Dtos;
using School.Data.Entities.Identity;

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
    }
}
