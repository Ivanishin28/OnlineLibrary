using MediatR;
using Shared.Core.Models;
using ShelfContext.Contract.Commands.ReshelveBook;
using ShelfContext.Domain.Entities.Base;
using ShelfContext.Domain.Entities.ShelvedBooks;
using ShelfContext.Domain.Entities.Shelves;
using ShelfContext.Domain.Interfaces;
using ShelfContext.Domain.Interfaces.Repositories;

namespace ShelfContext.UseCases.Commands
{
    public class ReshelveBookRequestHandler
        : IRequestHandler<ReshelveBookRequest, Result>
    {
        private IUnitOfWork _unitOfWork;
        private IShelfRepository _shelfRepository;
        private IShelvedBookRepository _shelvedBookRepository;

        public ReshelveBookRequestHandler(
            IShelfRepository shelfRepository, 
            IUnitOfWork unitOfWork, 
            IShelvedBookRepository shelvedBookRepository)
        {
            _shelfRepository = shelfRepository;
            _unitOfWork = unitOfWork;
            _shelvedBookRepository = shelvedBookRepository;
        }

        public async Task<Result> Handle(ReshelveBookRequest request, CancellationToken cancellationToken)
        {
            var shelfId = new ShelfId(request.ShelfId);
            var newShelf = await _shelfRepository.GetBy(shelfId);

            if (newShelf is null)
            {
                return Result.Failure(EntityErrors.NotFound);
            }

            var shelvedBookId = new ShelvedBookId(request.ShelvedBookId);
            var shelvedBook = await _shelvedBookRepository.GetBy(shelvedBookId);

            if (shelvedBook is null)
            {
                return Result.Failure(EntityErrors.NotFound);
            }

            shelvedBook.ReshelveTo(newShelf.Id);

            await _unitOfWork.SaveChanges();

            return Result.Success();
        }
    }
}
