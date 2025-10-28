using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace ShelfContext.Contract.Dtos
{
    public record BookReviewStatistics
    {
        [JsonPropertyName("avg_rating")]
        public required double AvgRating { get; init; }
        [JsonPropertyName("review_count")]
        public required int ReviewCount { get; init; }
    }
}
