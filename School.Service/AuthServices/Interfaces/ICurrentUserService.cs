using School.Data.Entities.Identity;

namespace School.Service.AuthServices.Interfaces
{
    public interface ICurrentUserService
    {
        public int GetUserId();
        public Task<User> GetUserAsync();
        public Task<List<string>> GetCurrentUserRolesAsync();
    }
}
