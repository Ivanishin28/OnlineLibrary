using MediatR;
using Shared.Core.Extensions;
using Shared.Core.Interfaces;
using Shared.Core.Models;
using ShelfContext.Contract.Commands.ShelveBook;
using ShelfContext.Domain.Entities.Base;
using ShelfContext.Domain.Entities.Books;
using ShelfContext.Domain.Entities.ShelvedBooks;
using ShelfContext.Domain.Entities.Shelves;
using ShelfContext.Domain.Entities.Users;
using ShelfContext.Domain.Interfaces;
using ShelfContext.Domain.Interfaces.Repositories;
using ShelfContext.Domain.Interfaces.Services;

namespace ShelfContext.UseCases.Commands
{
    public class ShelveBookRequestHandler
        : IRequestHandler<ShelveBookRequest, Result<Guid?>>
    {
        private IUnitOfWork _unitOfWork;
        private IShelfRepository _shelfRepository;
        private IBookAccessor _bookRepository;
        private IShelvedBookRepository _shelvedBookRepository;
        private IUserContext _userContext;
        private IShelvingService _shelvingService;

        public ShelveBookRequestHandler(
            IUnitOfWork unitOfWork,
            IShelfRepository shelfRepository,
            IBookAccessor bookRepository,
            IShelvedBookRepository shelvedBook,
            IUserContext userContext,
            IShelvingService shelvingService)
        {
            _unitOfWork = unitOfWork;
            _shelfRepository = shelfRepository;
            _bookRepository = bookRepository;
            _shelvedBookRepository = shelvedBook;
            _userContext = userContext;
            _shelvingService = shelvingService;
        }

        public async Task<Result<Guid?>> Handle(ShelveBookRequest request, CancellationToken cancellationToken)
        {
            var bookId = new BookId(request.BookId);
            var shelfId = new ShelfId(request.ShelfId);

            var shelf = await _shelfRepository.GetBy(shelfId);

            if (shelf is null)
            {
                return Result<Guid?>.Failure(EntityErrors.NotFound);
            }

            var book = await _bookRepository.GetBy(bookId);

            if (book is null)
            {
                return Result<Guid?>.Failure(EntityErrors.NotFound);
            }

            var shelvedBook = await _shelvedBookRepository.GetBy(new UserId(_userContext.UserId), bookId);

            if (shelvedBook is null)
            {
                var result = _shelvingService.Shelve(shelf, book);
                if (result.IsFailure)
                {
                    return result.ToFailure<Guid?>();
                }

                shelvedBook = result.Model;
                _shelvedBookRepository.Add(shelvedBook);
            }
            else
            {
                var result = _shelvingService.Reshelve(shelvedBook, shelf);
                if (result.IsFailure)
                {
                    return result.ToFailure<Guid?>();
                }
            }

            await _unitOfWork.SaveChanges();

            return shelvedBook.Id.Value;
        }
    }
}
