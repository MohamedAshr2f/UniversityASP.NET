using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using School.Data.Entities.Identity;
using School.Data.Helpers;
using School.Data.Results;
using School.Infrastructure.Abstracts;
using School.Service.Abstracts;

namespace School.Service.Implementions
{
    public class AuthenticationService : IAuthenticationService
    {
        private readonly UserManager<User> _userManager;
        private readonly IRefreshTokenRepository _refreshTokenRepository;
        private readonly JwtSettings _jwtSettings;

        public AuthenticationService(UserManager<User> userManager, IRefreshTokenRepository refreshTokenRepository, JwtSettings jwtSettings)
        {
            _userManager = userManager;
            _refreshTokenRepository = refreshTokenRepository;
            _jwtSettings = jwtSettings;


        }

        public async Task<JwtAuthResult> GetJWTToken(User user)
        {

            var (Jwttoken, accessToken) = await GenerateJwtToken(user);
            var refreshToken = GetRefreshToken(user.UserName);
            var userRefreshToken = new UserRefreshToken
            {
                AddedTime = DateTime.Now,
                ExpiryDate = DateTime.Now.AddDays(_jwtSettings.RefreshTokenExpireDate),
                IsUsed = true,
                IsRevoked = false,
                JwtId = Jwttoken.Id,
                RefreshToken = refreshToken.TokenString,
                Token = accessToken,
                UserId = user.Id
            };
            await _refreshTokenRepository.AddAsync(userRefreshToken);
            var result = new JwtAuthResult();
            result.AccessToken = accessToken;
            result.refreshToken = refreshToken;
            return result;
        }
        private RefreshToken GetRefreshToken(string username)
        {
            var refreshToken = new RefreshToken
            {
                ExpireAt = DateTime.Now.AddDays(_jwtSettings.RefreshTokenExpireDate),
                UserName = username,
                TokenString = GenerateRefreshToken()
            };
            return refreshToken;
        }
        private string GenerateRefreshToken()
        {
            var randomnumber = new byte[64];
            var randomNumberGenerate = RandomNumberGenerator.Create();
            randomNumberGenerate.GetBytes(randomnumber);
            return Convert.ToBase64String(randomnumber);
        }


        private async Task<(JwtSecurityToken, string)> GenerateJwtToken(User user)
        {
            var claims = GetClaims(user);
            var Jwttoken = new JwtSecurityToken(
                _jwtSettings.Issuer,
                _jwtSettings.Audience,
                claims,
                expires: DateTime.Now.AddDays(_jwtSettings.AccessTokenExpireDate),
                signingCredentials: new SigningCredentials(new SymmetricSecurityKey(Encoding.ASCII.GetBytes(_jwtSettings.Secret)), SecurityAlgorithms.HmacSha256Signature));
            var accessToken = new JwtSecurityTokenHandler().WriteToken(Jwttoken);
            return (Jwttoken, accessToken);

        }
        public List<Claim> GetClaims(User user)
        {
            var claims = new List<Claim>()
           {
               new Claim(nameof(UserClaimModels.UserName), user.UserName),
               new Claim(nameof(UserClaimModels.Email), user.Email),
               new Claim(nameof(UserClaimModels.PhoneNumber), user.PhoneNumber),
               new Claim(nameof(UserClaimModels.ID), user.Id.ToString())

           };
            return claims;
        }
        public async Task<JwtAuthResult> GetRefreshToken(User user, JwtSecurityToken jwtToken, DateTime? expiryDate, string refreshtoken)
        {

            var (jwtSecurityToken, newToken) = await GenerateJwtToken(user);
            var response = new JwtAuthResult();
            response.AccessToken = newToken;
            var refreshTokenResult = new RefreshToken();
            refreshTokenResult.UserName = jwtToken.Claims.FirstOrDefault(jc => jc.Type == nameof(UserClaimModels.UserName)).Value;
            refreshTokenResult.TokenString = refreshtoken;
            refreshTokenResult.ExpireAt = (DateTime)expiryDate;
            response.refreshToken = refreshTokenResult;

            return response;
        }
        public JwtSecurityToken ReadJWTToken(string accessToken)
        {
            if (string.IsNullOrEmpty(accessToken))
            {
                throw new ArgumentNullException(nameof(accessToken));
            }
            var handler = new JwtSecurityTokenHandler();
            var response = handler.ReadJwtToken(accessToken);
            return response;
        }

        public async Task<(string, DateTime?)> ValidateDetails(JwtSecurityToken jwtToken, string accesstoken, string refreshtoken)
        {
            if (jwtToken == null || !jwtToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha256Signature))
            {
                return ("AlgorithmIsWrong", null);
            }
            if (jwtToken.ValidTo > DateTime.UtcNow)
            {
                return ("TokenIsNotExpired", null);

            }
            //get user
            var userid = jwtToken.Claims.FirstOrDefault(jc => jc.Type == nameof(UserClaimModels.ID)).Value;

            var userRefreshToken = await _refreshTokenRepository.GetTableNoTracking().FirstOrDefaultAsync(u => u.Token == accesstoken
                                                                                                 && u.RefreshToken == refreshtoken
                                                                                                 && u.UserId == int.Parse(userid));
            if (userRefreshToken == null)
            {
                return ("RefreshTokenIsNotFound", null);
            }
            //validation token ,RefreshToken

            if (userRefreshToken.ExpiryDate < DateTime.UtcNow)
            {
                userRefreshToken.IsRevoked = true;
                userRefreshToken.IsUsed = false;
                await _refreshTokenRepository.UpdateAsync(userRefreshToken);
                return ("RefreshTokenIsExpired", null);
            }
            /* var user = await _userManager.FindByIdAsync(userid);
              if (user == null)
              {
                  throw new SecurityTokenException("USer Is Not Found");
              }*/
            var expirydate = userRefreshToken.ExpiryDate;
            return (userid, expirydate);
        }
        public async Task<string> ValidateToken(string accessToken)
        {
            var handler = new JwtSecurityTokenHandler();
            var parameters = new TokenValidationParameters
            {
                ValidateIssuer = _jwtSettings.ValidateIssuer,
                ValidIssuers = new[] { _jwtSettings.Issuer },
                ValidateIssuerSigningKey = _jwtSettings.ValidateIssuerSigningKey,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(_jwtSettings.Secret)),
                ValidAudience = _jwtSettings.Audience,
                ValidateAudience = _jwtSettings.ValidateAudience,
                ValidateLifetime = _jwtSettings.ValidateLifeTime,

            };
            try
            {
                var validator = handler.ValidateToken(accessToken, parameters, out SecurityToken validatedToken);
                if (validatedToken == null)
                {
                    return "InvalidToken";
                }
                return "NotExpired";
            }
            catch (Exception ex) { return ex.Message; }
        }


    }
}
