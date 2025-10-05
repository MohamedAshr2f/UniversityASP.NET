using System.IdentityModel.Tokens.Jwt;
using School.Data.Entities.Identity;
using School.Data.Results;

namespace School.Service.Abstracts
{
    public interface IAuthenticationService
    {
        public Task<JwtAuthResult> GetJWTToken(User user);
        public Task<JwtAuthResult> GetRefreshToken(string accesstoken, string refreshtoken);
        public Task<string> ValidateToken(string accessToken);
        public JwtSecurityToken ReadJWTToken(string accessToken);
    }
}
