using Microsoft.AspNetCore.Identity;
using School.Data.Entities.Identity;
using School.Service.Abstracts;

namespace School.Service.Implementions
{
    public class AuthorizationService : IAuthorizationService
    {
        private readonly RoleManager<Role> _roleManager;

        public AuthorizationService(RoleManager<Role> roleManager)
        {
            _roleManager = roleManager;
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
        public async Task<bool> RoleIsExist(string rolename)
        {
            return await _roleManager.RoleExistsAsync(rolename);
        }
    }
}
