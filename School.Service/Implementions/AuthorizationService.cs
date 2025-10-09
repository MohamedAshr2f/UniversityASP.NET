using System.Security.Claims;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using School.Data.Dtos;
using School.Data.Entities.Identity;
using School.Infrastructure.ApplicationContext;
using School.Service.Abstracts;
using SchoolProject.Data.Helpers;
using SchoolProject.Data.Results;

namespace School.Service.Implementions
{
    public class AuthorizationService : IAuthorizationService
    {
        private readonly Context _context;
        private readonly RoleManager<Role> _roleManager;
        private readonly UserManager<User> _userManager;

        public AuthorizationService(Context context, RoleManager<Role> roleManager, UserManager<User> userManager)
        {
            _context = context;
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

        public async Task<Role> GetRoleById(int roleId)
        {
            return await _roleManager.FindByIdAsync(roleId.ToString());
        }

        public async Task<List<Role>> GetRolesList()
        {
            return await _roleManager.Roles.ToListAsync();
        }

        public async Task<ManageUserClaimsResult> ManageUserClaimData(User user)
        {
            var response = new ManageUserClaimsResult();
            var usercliamsList = new List<UserClaims>();
            response.UserId = user.Id;
            //Get USer Claims
            var userClaims = await _userManager.GetClaimsAsync(user); //edit
                                                                      //create edit get print
            foreach (var claim in ClaimsStore.claims)
            {
                var userclaim = new UserClaims();
                userclaim.Type = claim.Type;
                if (userClaims.Any(x => x.Type == claim.Type))
                {
                    userclaim.Value = true;
                }
                else
                {
                    userclaim.Value = false;
                }
                usercliamsList.Add(userclaim);
            }
            response.userClaims = usercliamsList;
            //return Result
            return response;
        }

        public async Task<ManageUserRolesResult> ManageUserRolesData(User user)
        {
            var response = new ManageUserRolesResult();
            response.UserId = user.Id;
            var roles = await _roleManager.Roles.ToListAsync();
            var rolelist = new List<UserRoles>();
            foreach (var role in roles)
            {
                var userrole = new UserRoles();
                userrole.Id = role.Id;
                userrole.Name = role.Name;
                if (await _userManager.IsInRoleAsync(user, role.Name))
                {
                    userrole.HasRole = true;
                }
                else
                {
                    userrole.HasRole = false;
                }

                rolelist.Add(userrole);

            }
            response.userRoles = rolelist;
            return response;
        }


        public async Task<bool> RoleIsExist(string rolename)
        {
            return await _roleManager.RoleExistsAsync(rolename);
        }

        public async Task<string> UpdateUserClaims(UpdateUserClaimsRequest request)
        {

            var transact = await _context.Database.BeginTransactionAsync();
            try
            {
                var user = await _userManager.FindByIdAsync(request.UserId.ToString());
                if (user == null)
                {
                    return "UserIsNull";
                }
                //remove old Claims
                var userClaims = await _userManager.GetClaimsAsync(user);
                var removeClaimsResult = await _userManager.RemoveClaimsAsync(user, userClaims);
                if (!removeClaimsResult.Succeeded)
                    return "FailedToRemoveOldClaims";
                var claims = request.userClaims.Where(x => x.Value == true).Select(x => new Claim(x.Type, x.Value.ToString()));

                var addUserClaimResult = await _userManager.AddClaimsAsync(user, claims);
                if (!addUserClaimResult.Succeeded)
                    return "FailedToAddNewClaims";

                await transact.CommitAsync();
                return "Success";
            }
            catch (Exception ex)
            {
                await transact.RollbackAsync();
                return "FailedToUpdateClaims";
            }

        }

        public async Task<string> UpdateUserRoles(UpdateUserRolesRequest request)
        {
            var transact = await _context.Database.BeginTransactionAsync();
            try
            {
                //Get User
                var user = await _userManager.FindByIdAsync(request.UserId.ToString());
                if (user == null)
                {
                    return "UserIsNull";
                }
                //get user Old Roles
                var userRoles = await _userManager.GetRolesAsync(user);
                //Delete OldRoles
                var removeResult = await _userManager.RemoveFromRolesAsync(user, userRoles);
                if (!removeResult.Succeeded)
                    return "FailedToRemoveOldRoles";
                var selectedRoles = request.userRoles.Where(x => x.HasRole == true).Select(x => x.Name);

                //Add the Roles HasRole=True
                var addRolesresult = await _userManager.AddToRolesAsync(user, selectedRoles);
                if (!addRolesresult.Succeeded)
                    return "FailedToAddNewRoles";
                await transact.CommitAsync();
                //return Result
                return "Success";
            }
            catch (Exception ex)
            {
                await transact.RollbackAsync();
                return "FailedToUpdateUserRoles";
            }
        }
    }
}
