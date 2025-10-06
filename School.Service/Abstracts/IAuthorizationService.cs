namespace School.Service.Abstracts
{
    public interface IAuthorizationService
    {
        public Task<string> AddRoleAsync(string rolename);
        public Task<bool> RoleIsExist(string rolename);
    }
}
