using Mall.Common.Ioc.IocOptions;
using Mall.Core.Consts;
using Mall.Interface.Jwt;
using Mall.Model.Models;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.IO;
using System.Security.Claims;
using System.Text;

namespace Mall.Service.Jwt
{
    public class CustomHSJwtService : ICustomJwtService
    {
        private readonly JwtTokenOptions jwtTokenOptions;

        public CustomHSJwtService(IOptionsMonitor<JwtTokenOptions> configInformation)
        {
            this.jwtTokenOptions = configInformation.CurrentValue;
        }
        public string GetToken(Member user)
        {
            var claims = new[]
            {
                 new Claim(AuthConst.AUTH_NAME, user.Name),
                 new Claim(AuthConst.AUTH_ID, user.Id.ToString())
            };

            //需要加密：需要加密key:
            SymmetricSecurityKey key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtTokenOptions.SecurityKey));
            SigningCredentials creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            JwtSecurityToken token = new JwtSecurityToken(
             issuer: jwtTokenOptions.Issuer,
             audience: jwtTokenOptions.Audience,
             claims: claims,
             expires: DateTime.Now.AddMinutes(5),
             signingCredentials: creds);
            string returnToken = new JwtSecurityTokenHandler().WriteToken(token);
            return returnToken;
        }
    }
}
