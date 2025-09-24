using IdentityContext.Application.Consts;
using IdentityContext.Application.Interfaces;
using IdentityContext.Application.Models;
using IdentityContext.DL.Entities.ApplicationUser;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace IdentityContext.Application.Concrete
{
    public class JWTTokenBuilder : ITokenBuilder
    {
        public Token BuildFor(ApplicationUser user)
        {
            var claims = new[]
            {
                new Claim(Claims.USER_ID, user.UserId!.Value.ToString()),
                new Claim(Claims.IDENTITY_ID, user.Id.ToString())
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("this_is_a_much_longer_secret_key_1234567890"));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: "yourdomain.com",
                audience: "yourdomain.com",
                claims: claims,
                expires: DateTime.Now.AddMinutes(30),
                signingCredentials: creds);

            var tokenString = new JwtSecurityTokenHandler().WriteToken(token);
            return new Token(tokenString);
        }
    }
}
