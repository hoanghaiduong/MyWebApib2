using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using MyWebApi.Application.Interfaces;
using MyWebApi.Infrastructure.Models;

namespace MyWebApi.Application.Services
{
    public class JwtService : IJwtService
    {
        private readonly AppSettings _appSettings;

        public JwtService(IOptions<AppSettings> appSettings)
        {
            _appSettings = appSettings.Value;
        }

        public string GenerateAccessToken(IEnumerable<Claim> claims)
        {
            // Console.WriteLine(_appSettings.AccessTokenSecret);
            var key = new
            SymmetricSecurityKey(Encoding.ASCII.GetBytes(_appSettings.AccessTokenSecret));

            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.UtcNow.AddSeconds(15),
                Issuer = _appSettings.Issuer,
                Audience = _appSettings.Audience,
                SigningCredentials = creds
            };
            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }

        public string GenerateRefreshToken()
        {

            // Sử dụng RNGCryptoServiceProvider để tạo refresh token an toàn
            var randomNumber = new byte[32];
            using var rng = RandomNumberGenerator.Create();
            rng.GetBytes(randomNumber);
            return Convert.ToBase64String(randomNumber);
        }

        public ClaimsPrincipal GetPrincipalFromExpiredToken(string token)
        {
            //validate 
            var tokenValidationParameters = new TokenValidationParameters
            {
                ValidateAudience = true, //audience
                ValidAudience = _appSettings.Audience,
                ValidateIssuer = true,
                ValidIssuer = _appSettings.Issuer,
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(_appSettings.AccessTokenSecret)),
                ValidateLifetime = false // Chấp nhận token hết hạn để có thể refresh
            };
            var tokenHandler = new JwtSecurityTokenHandler();
            var principal = tokenHandler.ValidateToken(token,
            tokenValidationParameters, out SecurityToken securityToken);

            if (securityToken is not JwtSecurityToken jwtSecurityToken ||
            !jwtSecurityToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha256,
            StringComparison.InvariantCultureIgnoreCase))
                throw new SecurityTokenException("Invalid token");
            return principal;
        }

    }

}