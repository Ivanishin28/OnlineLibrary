using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace MediaContext.Application.Dtos
{
    public class MediaFileDto
    {
        [JsonPropertyName("id")]
        public Guid Id { get; set; }
        [JsonPropertyName("content")]
        public byte[] Content { get; set; } = null!;
        [JsonPropertyName("content_type")]
        public string ContentType { get; set; } = null!;
    }
}
