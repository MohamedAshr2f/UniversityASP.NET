using School.Data.Entities.Identity;

namespace School.Service.Abstracts
{
    public interface IAuthenticationService
    {
        public Task<string> GetJWTToken(User user);
    }
}
