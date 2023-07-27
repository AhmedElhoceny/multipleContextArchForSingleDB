using AuthModule.DTOs.User.Request;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace AuthModule.Helpers
{
    public static class TokenServices
    {
        private static readonly string SecretKey = "ghm1c031f3/TsXN49yW3Mvzc/YrXusCXCrVcDzn/oQA=";
        private static readonly string Issuer = "SAAS";
        public static string GenerateJSONWebToken(UserTokenInfo userInfo)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(SecretKey));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var claims = new List<Claim>() {
                new Claim("companyId",userInfo.companyId.ToString()),
                new Claim("userId",userInfo.userId.ToString()),
                new Claim("Permissions",string.Join(",",userInfo.Permissions))
            };

            var token = new JwtSecurityToken(Issuer,
              Issuer,
              claims,
              expires: DateTime.Now.AddMinutes(120),
              signingCredentials: credentials);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
