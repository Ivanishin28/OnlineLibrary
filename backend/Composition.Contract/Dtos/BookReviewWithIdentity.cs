using IdentityContext.Contracts.Dtos;
using ShelfContext.Contract.Dtos;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Text.Json.Serialization;

namespace Composition.Contract.Dtos
{
    public record BookReviewWithIdentity : ReviewDto
    {
        [JsonPropertyName("identity")]
        public IdentityPreviewDto Identity { get; init; }

        [SetsRequiredMembers]
        public BookReviewWithIdentity(ReviewDto original, IdentityPreviewDto identity) : base(original)
        {
            Identity = identity;
        }
    }
}
