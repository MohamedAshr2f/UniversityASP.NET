using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using School.Data.Entities.Identity;
using School.Data.Helpers;
using School.Service.Abstracts;

namespace School.Service.Implementions
{
    public class AuthenticationService : IAuthenticationService
    {
        private readonly JwtSettings _jwtSettings;


        public AuthenticationService(JwtSettings jwtSettings)
        {
            _jwtSettings = jwtSettings;

        }

        public async Task<string> GetJWTToken(User user)
        {
            var claims = new List<Claim>()
           {
               new Claim(nameof(UserClaimModels.UserName), user.UserName),
               new Claim(nameof(UserClaimModels.Email), user.Email),
               new Claim(nameof(UserClaimModels.PhoneNumber), user.PhoneNumber)

           };
            var Jwttoken = new JwtSecurityToken(
                _jwtSettings.Issuer,
                _jwtSettings.Audience,
                claims,
                expires: DateTime.Now.AddDays(_jwtSettings.AccessTokenExpireDate),
                signingCredentials: new SigningCredentials(new SymmetricSecurityKey(Encoding.ASCII.GetBytes(_jwtSettings.Secret)), SecurityAlgorithms.HmacSha256Signature));
            var accessToken = new JwtSecurityTokenHandler().WriteToken(Jwttoken);
            return accessToken;
        }
    }
}
