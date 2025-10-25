using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MediaContext.Application.Dtos
{
    public class MediaFileDto
    {
        public Guid Id { get; set; }
        public byte[] Context { get; set; } = null!;
    }
}
