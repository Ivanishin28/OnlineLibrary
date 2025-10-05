using MediatR;
using Shared.Core.Extensions;
using Shared.Core.Models;
using ShelfContext.Contract.Commands.ShelveBook;
using ShelfContext.Contract.Errors;
using ShelfContext.Contract.Services;
using ShelfContext.Domain.Entities.Books;
using ShelfContext.Domain.Entities.Shelves;
using ShelfContext.Domain.Entities.Users;
using ShelfContext.Domain.Interfaces;
using ShelfContext.Domain.Interfaces.Services;

namespace ShelfContext.UseCases.Commands
{
    public class ShelveBookRequestHandler
        : IRequestHandler<ShelveBookRequest, Result<Guid?>>
    {
        private IUnitOfWork _unitOfWork;
        private IResouceAccessibilityChecker _checker;
        private IShelvingService _shelvingService;

        public ShelveBookRequestHandler(
            IUnitOfWork unitOfWork,
            IResouceAccessibilityChecker checker,
            IShelvingService shelvingService)
        {
            _unitOfWork = unitOfWork;
            _checker = checker;
            _shelvingService = shelvingService;
        }

        public async Task<Result<Guid?>> Handle(ShelveBookRequest request, CancellationToken cancellationToken)
        {
            var bookId = new BookId(request.BookId);
            var shelfId = new ShelfId(request.ShelfId);
            var userId = new UserId(request.UserId);

            var canAccess = await CanAccess(userId, bookId, shelfId);
            if (canAccess.IsFailure)
            {
                return canAccess.ToFailure<Guid?>();
            }

            var result = await _shelvingService.Shelve(shelfId, bookId);

            if (result.IsFailure)
            {
                return result.ToFailure<Guid?>();
            }

            await _unitOfWork.SaveChanges();

            return result.Model.Value;
        }

        private async Task<Result> CanAccess(UserId userId, BookId bookId, ShelfId shelfId)
        {
            if (!await _checker.IsBookAccessibleToUser(bookId.Value, userId.Value))
            {
                return Result.Failure(AccessibilityErrors.CannotAccessBook(userId.Value, bookId.Value));
            }
            if (!await _checker.IsShelfAccesibleToUser(shelfId.Value, userId.Value))
            {
                return Result.Failure(AccessibilityErrors.CannotAccessShelf(userId.Value, shelfId.Value));
            }

            return Result.Success();
        }
    }
}
