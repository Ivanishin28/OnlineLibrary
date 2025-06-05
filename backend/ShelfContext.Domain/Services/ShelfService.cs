using MediatR;
using Shared.Core.Models;
using ShelfContext.Domain.Entities.Base;
using ShelfContext.Domain.Entities.Books;
using ShelfContext.Domain.Entities.ShelvedBooks;
using ShelfContext.Domain.Entities.Shelves;
using ShelfContext.Domain.Interfaces.Queries.IsBookShelvedForUser;
using ShelfContext.Domain.Interfaces.Repositories;
using ShelfContext.Domain.Interfaces.Services;

namespace ShelfContext.Domain.Services
{
    public class ShelfService : IShelfService
    {
        private IMediator _mediator;
        private IShelfRepository _shelfRepository;

        public ShelfService(IShelfRepository shelfRepository, IMediator mediator)
        {
            _shelfRepository = shelfRepository;
            _mediator = mediator;
        }

        public async Task<Result<ShelvedBook>> ShelveBook(ShelfId shelfId, BookId bookId)
        {
            var shelf = await _shelfRepository.GetBy(shelfId);

            if(shelf is null)
            {
                return Result<ShelvedBook>.Failure(EntityErrors.NotFound);
            }

            var query = new IsBookShelvedForUserQuery(bookId, shelf.UserId);
            var isAlreadyShelved = await _mediator.Send(query);

            if(isAlreadyShelved)
            {
                return Result<ShelvedBook>.Failure(ShelfErrors.AlreadyShelved);
            }

            return ShelvedBook.Create(shelfId, bookId);
        }
    }
}
