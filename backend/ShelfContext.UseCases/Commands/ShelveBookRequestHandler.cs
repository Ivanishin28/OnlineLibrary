using MediatR;
using Shared.Core.Extensions;
using Shared.Core.Interfaces;
using Shared.Core.Models;
using ShelfContext.Contract.Commands.ShelveBook;
using ShelfContext.Contract.Errors;
using ShelfContext.Domain.Entities.Base;
using ShelfContext.Domain.Entities.Books;
using ShelfContext.Domain.Entities.ShelvedBooks;
using ShelfContext.Domain.Entities.Shelves;
using ShelfContext.Domain.Entities.Users;
using ShelfContext.Domain.Interfaces;
using ShelfContext.Domain.Interfaces.Queries;
using ShelfContext.Domain.Interfaces.Repositories;
using ShelfContext.UseCases.Exceptions;

namespace ShelfContext.UseCases.Commands
{
    public class ShelveBookRequestHandler
        : IRequestHandler<ShelveBookRequest, Result<Guid?>>
    {
        private IUnitOfWork _unitOfWork;
        private IShelfRepository _shelfRepository;
        private IBookAccessor _bookRepository;
        private IShelvedBookRepository _shelvedBookRepository;
        private IResouceAccessibilityChecker _checker;

        public ShelveBookRequestHandler(
            IUnitOfWork unitOfWork,
            IShelfRepository shelfRepository,
            IBookAccessor bookRepository,
            IShelvedBookRepository shelvedBook,
            IResouceAccessibilityChecker checker)
        {
            _unitOfWork = unitOfWork;
            _shelfRepository = shelfRepository;
            _bookRepository = bookRepository;
            _shelvedBookRepository = shelvedBook;
            _checker = checker;
        }

        public async Task<Result<Guid?>> Handle(ShelveBookRequest request, CancellationToken cancellationToken)
        {
            var bookId = new BookId(request.BookId);
            var shelfId = new ShelfId(request.ShelfId);
            var userId = new UserId(request.UserId);

            if (!await _checker.IsAccessible(bookId, userId) || 
                !await _checker.IsAccessible(shelfId, userId))
            {
                return Result<Guid?>.Failure(AccessibilityErrors.CANNOT_ACCESS_RESOUCE);
            }

            var shelf = await _shelfRepository.GetBy(shelfId);
            var book = await _bookRepository.GetBy(bookId);

            if (shelf is null || book is null)
            {
                throw new ExpectedResouceUnavailableException();
            }

            var shelvedBook = await _shelvedBookRepository.GetBy(new UserId(request.UserId), bookId);

            if (shelvedBook is null)
            {
                shelvedBook = shelf.Shelve(bookId);
                _shelvedBookRepository.Add(shelvedBook);
            }
            else
            {
                var result = shelvedBook.ReshelveTo(shelf);
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
