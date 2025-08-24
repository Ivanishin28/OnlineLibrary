using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace BookContext.Contract.Dtos
{
    public record BookPreviewDto
    {
        [JsonPropertyName("id")]
        public Guid Id { get; init; }
        [JsonPropertyName("title")]
        public string Title { get; init; }

        public BookPreviewDto(Guid id, string title)
        {
            Id = id;
            Title = title;
        }
    }
}
