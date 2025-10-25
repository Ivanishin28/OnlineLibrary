using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MediaContext.DL.Entities
{
    public class MediaFile
    {
        public Guid Id { get; set; }
        public byte[] Cotnext { get; set; } = null!;

        public MediaFile()
        {
        }

        public MediaFile(Guid id, byte[] cotnext)
        {
            Id = id;
            Cotnext = cotnext;
        }
    }
}
