using Flinnt.Business.Helpers;
using Flinnt.Business.ViewModels;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace AAT.API.Helpers
{
    public class ApiTokenHelper
    {
        //public static string GenerateJSONWebToken(ApplicationUser user)
        //{
        //    var appSettings = new Jwt();
        //    var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(appSettings.Key));
        //    var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
        //    var claims = new[] {
        //    new Claim("Id", Convert.ToString(user.Id)),
        //    new Claim("Name",user.UserName),
        //    new Claim(JwtRegisteredClaimNames.Jti,Guid.NewGuid().ToString())
        //    };
        //    var token = new JwtSecurityToken(
        //        issuer: appSettings.Issuer,
        //        audience: appSettings.Issuer,
        //        claims,
        //        expires: DateTime.Now.AddMinutes(120),
        //        signingCredentials: credentials);

        //    var encodeToken = new JwtSecurityTokenHandler().WriteToken(token);
        //    return encodeToken;
        //}
    }
}
