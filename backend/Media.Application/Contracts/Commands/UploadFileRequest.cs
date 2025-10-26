using MediatR;
using System.Text.Json.Serialization;

namespace MediaContext.Application.Contracts.Commands
{
    public class UploadFileRequest : IRequest<Guid?>
    {
        [JsonPropertyName("content")]
        public byte[] Content { get; set; } = null!;
        [JsonPropertyName("content_type")]
        public string ContentType { get; set; } = null!;
    }
}
