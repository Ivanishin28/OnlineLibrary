using IdentityContext.Application.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace IdentityContext.Application.Configuration
{
    public static class AuthenticationConfiguration
    {
        public static IServiceCollection AddJWTAuthentication(
            this IServiceCollection services, 
            ConfigurationManager config)
        {
            var tokenConfig = new JwtTokenConfig(
                "yourdomain.com", 
                "yourdomain.com", 
                "this_is_a_much_longer_secret_key_1234567890",
                null);

            services.AddSingleton(tokenConfig);

            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = true,
                        ValidateAudience = true,
                        ValidateLifetime = tokenConfig.LifeTime.HasValue,
                        ValidateIssuerSigningKey = true,
                        ValidIssuer = tokenConfig.Issuer,
                        ValidAudience = tokenConfig.Audience,
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(tokenConfig.IssuerSigningKey))
                    };
                });
            return services;
        }
    }
}
