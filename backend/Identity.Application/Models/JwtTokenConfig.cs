using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IdentityContext.Application.Models
{
    public record JwtTokenConfig
    {
        public string Issuer { get; private set; }
        public string Audience { get; private set; }
        public string IssuerSigningKey { get; private set; }
        public TimeSpan? LifeTime { get; private set; }

        public JwtTokenConfig(string issuer, string audience, string issuerSigningKey, TimeSpan? lifeTime)
        {
            Issuer = issuer;
            Audience = audience;
            IssuerSigningKey = issuerSigningKey;
            LifeTime = lifeTime;
        }
    }
}
