using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IdentityContext.Contracts.Dtos
{
    public record IdentityPreviewDto
    {
        public required string Nickname { get; init; }
    }
}
