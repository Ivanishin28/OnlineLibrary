using MediatR;
using Shared.Core.Extensions;
using Shared.Core.Models;
using ShelfContext.Contract.Commands.ShelveBook;
using ShelfContext.Domain.Entities.Books;
using ShelfContext.Domain.Entities.Shelves;
using ShelfContext.Domain.Interfaces;
using ShelfContext.Domain.Interfaces.Repositories;
using ShelfContext.Domain.Interfaces.Services;

namespace ShelfContext.UseCases.Commands
{
    public class ShelveBookRequestHandler
        : IRequestHandler<ShelveBookRequest, Result<ShelveBookResponse>>
    {
        private IShelvedBookRepository _shelvedBookRepository;
        private IShelfService _shelfService;
        private IUnitOfWork _unitOfWork;

        public ShelveBookRequestHandler(
            IShelvedBookRepository shelvedBookRepository,
            IUnitOfWork unitOfWork,
            IShelfService shelfService)
        {
            _shelvedBookRepository = shelvedBookRepository;
            _unitOfWork = unitOfWork;
            _shelfService = shelfService;
        }

        public async Task<Result<ShelveBookResponse>> Handle(ShelveBookRequest request, CancellationToken cancellationToken)
        {
            var bookId = new BookId(request.BookId);
            var shelfId = new ShelfId(request.ShelfId);

            var shelvedBookResult = await _shelfService.ShelveBook(shelfId, bookId);

            if (shelvedBookResult.IsFailure)
            {
                return shelvedBookResult.ToFailure<ShelveBookResponse>();
            }

            var shelvedBook = shelvedBookResult.Model;
            await _shelvedBookRepository.Add(shelvedBook);

            await _unitOfWork.SaveChanges();

            return new ShelveBookResponse(shelvedBook.Id.Value);
        }
    }
}
