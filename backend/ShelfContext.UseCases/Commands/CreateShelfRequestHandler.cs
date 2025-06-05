using MediatR;
using Shared.Core.Extensions;
using Shared.Core.Models;
using ShelfContext.Contract.Commands.CreateShelf;
using ShelfContext.Domain.DTOs;
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
        private IShelfCreationService _shelfCreationService;
        private IShelfRepository _shelfRepository;
        private IUnitOfWork _unitOfWork;

        public CreateShelfRequestHandler(
            IShelfCreationService shelfCreationService,
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

            await _shelfRepository.Add(shelf);

            await _unitOfWork.SaveChanges();

            return new CreateShelfResponse(shelf.Id.Value);
        }

        private async Task<Result<Shelf>> CreateShelf(CreateShelfRequest request)
        {
            var userId = new UserId(request.UserId);

            var dto = new ShelfDto(request.Name);

            return await _shelfCreationService.Create(userId, dto);
        }
    }
}
