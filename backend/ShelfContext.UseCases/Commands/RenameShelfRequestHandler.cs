using MediatR;
using Shared.Core.Models;
using ShelfContext.Contract.Commands;
using ShelfContext.Domain.Entities.Shelves;
using ShelfContext.Domain.Interfaces;
using ShelfContext.Domain.Interfaces.Repositories;
using ShelfContext.Domain.Interfaces.Services;

namespace ShelfContext.UseCases.Commands
{
    public class RenameShelfRequestHandler : IRequestHandler<RenameShelfRequest, Result>
    {
        private IShelfRepository _shelfRepository;
        private IShelfNameCreationService _nameCreator;
        private IUnitOfWork _unitOfWork;

        public RenameShelfRequestHandler(
            IShelfRepository shelfRepository,
            IUnitOfWork unitOfWork,
            IShelfNameCreationService nameCreator)
        {
            _shelfRepository = shelfRepository;
            _unitOfWork = unitOfWork;
            _nameCreator = nameCreator;
        }

        public async Task<Result> Handle(RenameShelfRequest request, CancellationToken cancellationToken)
        {
            var shelfId = new ShelfId(request.ShelfId);
            var shelf = await _shelfRepository.GetBy(shelfId);
            if (shelf is null)
            {
                return Result.Failure(ShelfErrors.NotFound(shelfId));
            }

            var name = await _nameCreator.Create(shelf.UserId, request.Name);
            if (name.IsFailure)
            {
                return name;
            }

            shelf.UpdateName(name.Model);

            await _unitOfWork.SaveChanges();
            return Result.Success();
        }
    }
}
