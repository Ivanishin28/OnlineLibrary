using BookContext.Contract.Dtos;
using BookContext.Contract.Queries;
using Composition.Contract.Dtos;
using Composition.Contract.Queries;
using MediatR;
using Shared.Contracts.Models;
using ShelfContext.Contract.Dtos;

namespace Composition.Application.UseCases.Queries
{
    public class GetLibraryPageQueryHandler : IRequestHandler<GetLibraryPageQuery, Pagination<LibraryShelvedBook>>
    {
        private IMediator _mediator;

        public GetLibraryPageQueryHandler(IMediator mediator)
        {
            _mediator = mediator;
        }

        public async Task<Pagination<LibraryShelvedBook>> Handle(GetLibraryPageQuery request, CancellationToken cancellationToken)
        {
            var shelvedBooksPage = await GetShelvedBooksBy(request);
            var books = await GetBooksFor(shelvedBooksPage.Items);
            var combination = Combine(shelvedBooksPage.Items, books);

            return new Pagination<LibraryShelvedBook>(shelvedBooksPage.Total, combination);
        }

        private Task<Pagination<ShelvedBookDto>> GetShelvedBooksBy(GetLibraryPageQuery request)
        {
            var query = new ShelfContext.Contract.Queries.GetLibraryPageQuery(
                request.UserId,
                request.Filter,
                request.Page);
            return _mediator.Send(query);
        }

        private Task<ICollection<BookPreviewDto>> GetBooksFor(ICollection<ShelvedBookDto> shelvedBooks)
        {
            var ids = shelvedBooks
                .Select(x => x.BookId)
                .ToList();
            var query = new GetBookPreviewListQuery(ids);
            return _mediator.Send(query);
        }

        private ICollection<LibraryShelvedBook> Combine(
            ICollection<ShelvedBookDto> shelvedBooks, 
            ICollection<BookPreviewDto> books)
        {
            return shelvedBooks
                .Where(sb => books.Any(book => sb.BookId == book.Id))
                .Select(sb =>
                {
                    var book = books.First(book => book.Id == sb.BookId);
                    return new LibraryShelvedBook(sb, book);
                })
                .ToList();
        }
    }
}
