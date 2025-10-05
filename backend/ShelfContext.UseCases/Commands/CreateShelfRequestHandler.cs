using MediatR;
using Shared.Core.Extensions;
using Shared.Core.Models;
using ShelfContext.Contract.Commands.CreateShelf;
using ShelfContext.Domain.Entities.Shelves;
using ShelfContext.Domain.Entities.Users;
using ShelfContext.Domain.Interfaces;
using ShelfContext.Domain.Interfaces.Repositories;
using ShelfContext.Domain.Interfaces.Services;

namespace ShelfContext.UseCases.Commands
{
    public class CreateShelfRequestHandler :
        IRequestHandler<CreateShelfRequest, Result<CreateShelfResponse>>
    {
        private IShelfNameCreationService _shelfCreationService;
        private IShelfRepository _shelfRepository;
        private IUnitOfWork _unitOfWork;

        public CreateShelfRequestHandler(
            IShelfNameCreationService shelfCreationService,
            IShelfRepository shelfRepository,
            IUnitOfWork unitOfWork)
        {
            _shelfCreationService = shelfCreationService;
            _shelfRepository = shelfRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<Result<CreateShelfResponse>> Handle(CreateShelfRequest request, CancellationToken cancellationToken)
        {
            var creationResult = await CreateShelf(request);

            if(creationResult.IsFailure)
            {
                return creationResult.ToFailure<CreateShelfResponse>();
            }

            var shelf = creationResult.Model;

            _shelfRepository.Add(shelf);

            await _unitOfWork.SaveChanges();

            return new CreateShelfResponse(shelf.Id.Value);
        }

        private async Task<Result<Shelf>> CreateShelf(CreateShelfRequest request)
        {
            var userId = new UserId(request.UserId);

            var nameResult = await _shelfCreationService.Create(userId, request.Name);

            if (nameResult.IsFailure)
            {
                return nameResult.ToFailure<Shelf>();
            }

            return Shelf.Create(userId, nameResult.Model);
        }
    }
}
