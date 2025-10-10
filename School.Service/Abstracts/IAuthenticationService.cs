using System.IdentityModel.Tokens.Jwt;
using School.Data.Entities.Identity;
using School.Data.Results;

namespace School.Service.Abstracts
{
    public interface IAuthenticationService
    {
        public Task<JwtAuthResult> GetJWTToken(User user);
        public Task<(string, DateTime?)> ValidateDetails(JwtSecurityToken jwtToken, string accesstoken, string refreshtoken);
        public Task<JwtAuthResult> GetRefreshToken(User user, JwtSecurityToken jwtToken, DateTime? expiryDate, string refreshToken);
        public Task<string> ValidateToken(string accessToken);
        public JwtSecurityToken ReadJWTToken(string accessToken);
        public Task<string> ConfirmEmail(int? userId, string? code);
    }
}
