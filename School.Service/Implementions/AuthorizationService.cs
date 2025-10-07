using Microsoft.AspNetCore.Identity;
using School.Data.Dtos;
using School.Data.Entities.Identity;
using School.Service.Abstracts;

namespace School.Service.Implementions
{
    public class AuthorizationService : IAuthorizationService
    {
        private readonly RoleManager<Role> _roleManager;
        private readonly UserManager<User> _userManager;

        public AuthorizationService(RoleManager<Role> roleManager, UserManager<User> userManager)
        {
            _roleManager = roleManager;
            _userManager = userManager;
        }
        public async Task<string> AddRoleAsync(string rolename)
        {
            var Identityrole = new Role();
            Identityrole.Name = rolename;

            var result = await _roleManager.CreateAsync(Identityrole);
            if (result.Succeeded)
            {
                return "Success";
            }
            return "Faild";

        }

        public async Task<string> DeleteRoleAsync(int roleId)
        {
            var role = await _roleManager.FindByIdAsync(roleId.ToString());
            if (role == null) return "NotFound";
            //Chech if user has this role or not
            var users = await _userManager.GetUsersInRoleAsync(role.Name);
            //return exception 
            if (users != null && users.Count() > 0) return "Used";
            //delete
            var result = await _roleManager.DeleteAsync(role);
            //success
            if (result.Succeeded) return "Success";
            //problem
            var errors = string.Join("-", result.Errors);
            return errors;
        }

        public async Task<string> EditRoleAsync(EditRequestCommand request)
        {
            var role = await _roleManager.FindByIdAsync(request.Id.ToString());
            if (role == null)
            {
                return "notfound";
            }
            role.Name = request.RoleName;
            var result = await _roleManager.UpdateAsync(role);
            if (result.Succeeded)
            {
                return "Success";
            }
            var errors = string.Join("-", result.Errors);
            return errors;

        }
        public async Task<bool> RoleIsExist(string rolename)
        {
            return await _roleManager.RoleExistsAsync(rolename);
        }
    }
}
