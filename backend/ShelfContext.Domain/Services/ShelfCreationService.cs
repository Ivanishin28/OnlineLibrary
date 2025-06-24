using MediatR;
using Shared.Core.Extensions;
using Shared.Core.Models;
using ShelfContext.Domain.DTOs;
using ShelfContext.Domain.Entities.Base;
using ShelfContext.Domain.Entities.Shelves;
using ShelfContext.Domain.Entities.Users;
using ShelfContext.Domain.Interfaces.Queries.IsShelfNameTakenByUser;
using ShelfContext.Domain.Interfaces.Repositories;
using ShelfContext.Domain.Interfaces.Services;

namespace ShelfContext.Domain.Services
{
    public class ShelfCreationService : IShelfCreationService
    {
        private IShelfRepository _shelfRepository;
        private IUserRepository _userRepository;
        private IMediator _mediator;
        public ShelfCreationService(
            IShelfRepository shelfRepository,
            IUserRepository userRepository,
            IMediator mediator)
        {
            _shelfRepository = shelfRepository;
            _userRepository = userRepository;
            _mediator = mediator;
        }

        public async Task<Result<Shelf>> Create(UserId userId, ShelfDto dto)
        {
            var userExists = await _userRepository.Exists(userId);

            if (!userExists)
            {
                return Result<Shelf>.Failure(EntityErrors.NotFound);
            }

            var nameResult = await CreateShelfName(userId, dto.Name);

            if(nameResult.IsFailure)
            {
                return nameResult.ToFailure<Shelf>();
            }

            return Shelf.Create(userId, nameResult.Model);
        }

        public async Task<Result> Update(ShelfId shelfId, ShelfDto shelfDto)
        {
            var shelf = await _shelfRepository.GetBy(shelfId);

            if(shelf is null)
            {
                return Result.Failure(EntityErrors.NotFound);
            }

            var nameResult = await CreateShelfName(shelf.UserId, shelfDto.Name);

            if(nameResult.IsFailure)
            {
                return nameResult;
            }

            shelf.UpdateName(nameResult.Model);

            return Result.Success();
        }

        private async Task<Result<ShelfName>> CreateShelfName(UserId userId, string name)
        {
            var nameResult = ShelfName.Create(name);

            if (nameResult.IsFailure)
            {
                return nameResult.ToFailure<ShelfName>();
            }

            var query = new IsShelfNameTakenByUserQuery(nameResult.Model, userId);
            var isShelfNameTaken = await _mediator.Send(query);

            if (isShelfNameTaken)
            {
                return Result<ShelfName>.Failure(ShelfErrors.DuplicateName);
            }

            return nameResult;
        }
    }
}
