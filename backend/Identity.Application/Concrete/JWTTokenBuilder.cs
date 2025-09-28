using IdentityContext.Application.Consts;
using IdentityContext.Application.Interfaces;
using IdentityContext.Application.Models;
using IdentityContext.DL.Entities.ApplicationUser;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace IdentityContext.Application.Concrete
{
    public class JwtTokenBuilder : ITokenBuilder
    {
        private JwtTokenConfig _config;

        public JwtTokenBuilder(JwtTokenConfig config)
        {
            _config = config;
        }

        public Token BuildFor(ApplicationUser user)
        {
            var claims = new[]
            {
                new Claim(Claims.USER_ID, user.UserId!.Value.ToString()),
                new Claim(Claims.IDENTITY_ID, user.Id.ToString())
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config.IssuerSigningKey));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: _config.Issuer,
                audience: _config.Audience,
                claims: claims,
                expires: _config.LifeTime.HasValue ? DateTime.Now.Add(_config.LifeTime.Value) : null,
                signingCredentials: creds);

            var tokenString = new JwtSecurityTokenHandler().WriteToken(token);
            return new Token(tokenString);
        }
    }
}
