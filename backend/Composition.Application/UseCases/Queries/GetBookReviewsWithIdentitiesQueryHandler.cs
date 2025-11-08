using Composition.Contract.Dtos;
using Composition.Contract.Queries;
using IdentityContext.Contracts.Dtos;
using IdentityContext.Contracts.Queries;
using MediatR;
using Shared.Contracts.Models;
using ShelfContext.Contract.Dtos;
using ShelfContext.Contract.Queries;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Composition.Application.UseCases.Queries
{
    public class GetBookReviewsWithIdentitiesQueryHandler
        : IRequestHandler<GetBookReviewsWithIdentitiesQuery, Pagination<BookReviewWithIdentity>>
    {
        private IMediator _mediator;

        public GetBookReviewsWithIdentitiesQueryHandler(IMediator mediator)
        {
            _mediator = mediator;
        }

        public async Task<Pagination<BookReviewWithIdentity>> Handle(GetBookReviewsWithIdentitiesQuery request, CancellationToken cancellationToken)
        {
            var bookQuery = new GetBookReviewsPageQuery(request.BookId, request.Page);
            var reviews = await _mediator.Send(bookQuery);
            if (!reviews.Items.Any())
            {
                return new Pagination<BookReviewWithIdentity>(reviews.Total, new List<BookReviewWithIdentity>());
            }

            var identities = await GetIdentitiesFor(reviews.Items);
            
            var items = Combine(reviews.Items, identities);
            return new Pagination<BookReviewWithIdentity>(reviews.Total, items);
        }

        private Task<ICollection<IdentityPreviewDto>> GetIdentitiesFor(ICollection<ReviewDto> reviews)
        {
            var userIds = reviews
                .Select(x => x.UserId)
                .ToList();
            var query = new GetIdentityPreviewsFromUserIdsQuery(userIds);
            return _mediator.Send(query);
        }

        private ICollection<BookReviewWithIdentity> Combine(
            ICollection<ReviewDto> reviews, 
            ICollection<IdentityPreviewDto> identities)
        {
            return reviews
                .Where(review => identities.Any(identity => identity.UserId == review.UserId))
                .Select(review =>
                {
                    var identity = identities.First(x => x.UserId == review.UserId);
                    return new BookReviewWithIdentity(review, identity);
                })
                .ToList();
        }
    }
}
