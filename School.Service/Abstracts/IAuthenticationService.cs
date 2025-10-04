using School.Data.Entities.Identity;
using School.Data.Results;

namespace School.Service.Abstracts
{
    public interface IAuthenticationService
    {
        public Task<JwtAuthResult> GetJWTToken(User user);
    }
}
