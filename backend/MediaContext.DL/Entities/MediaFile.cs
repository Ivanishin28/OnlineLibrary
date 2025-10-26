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
        public byte[] Content { get; set; } = null!;
        public string ContentType { get; set; } = null!;

        private MediaFile()
        {
        }

        public MediaFile(Guid id, byte[] cotnext, string contentType)
        {
            Id = id;
            Content = cotnext;
            ContentType = contentType;
        }
    }
}
