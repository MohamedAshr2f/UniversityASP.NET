using School.Data.Dtos;

namespace School.Service.Abstracts
{
    public interface IAuthorizationService
    {
        public Task<string> AddRoleAsync(string rolename);
        public Task<bool> RoleIsExist(string rolename);
        public Task<string> EditRoleAsync(EditRequestCommand request);
        public Task<string> DeleteRoleAsync(int roleId);
    }
}
