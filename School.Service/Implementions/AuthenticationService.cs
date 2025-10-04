using System.Collections.Concurrent;
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

        private readonly ConcurrentDictionary<string, RefreshToken> _userefreshTokens;

        public AuthenticationService(IRefreshTokenRepository refreshTokenRepository, JwtSettings jwtSettings)
        {
            _refreshTokenRepository = refreshTokenRepository;
            _jwtSettings = jwtSettings;
            _userefreshTokens = new ConcurrentDictionary<string, RefreshToken>();

        }

        public async Task<JwtAuthResult> GetJWTToken(User user)
        {
            var claims = GetClaims(user);
            var Jwttoken = new JwtSecurityToken(
                _jwtSettings.Issuer,
                _jwtSettings.Audience,
                claims,
                expires: DateTime.Now.AddDays(_jwtSettings.AccessTokenExpireDate),
                signingCredentials: new SigningCredentials(new SymmetricSecurityKey(Encoding.ASCII.GetBytes(_jwtSettings.Secret)), SecurityAlgorithms.HmacSha256Signature));
            var accessToken = new JwtSecurityTokenHandler().WriteToken(Jwttoken);

            var refreshToken = GetRefreshToken(user.UserName);
            //  _userefreshTokens.AddOrUpdate(refreshToken.TokenString, refreshToken, (S, t) => refreshToken);

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
    }
}
