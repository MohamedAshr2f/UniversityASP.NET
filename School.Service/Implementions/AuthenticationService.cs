using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
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
        private readonly IRefreshTokenRepository _refreshTokenRepository;
        private readonly JwtSettings _jwtSettings;

        public AuthenticationService(IRefreshTokenRepository refreshTokenRepository, JwtSettings jwtSettings)
        {
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
        public List<Claim> GetClaims(User user)
        {
            var claims = new List<Claim>()
           {
               new Claim(nameof(UserClaimModels.UserName), user.UserName),
               new Claim(nameof(UserClaimModels.Email), user.Email),
               new Claim(nameof(UserClaimModels.PhoneNumber), user.PhoneNumber)

           };
            return claims;
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
        public Task<JwtAuthResult> GetRefreshToken(string accesstoken, string refreshtoken)
        {
            //read token to get claims

            //get user

            //validation token ,RefreshToken

            //Generate RefreshToken
            throw new NotImplementedException();
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
