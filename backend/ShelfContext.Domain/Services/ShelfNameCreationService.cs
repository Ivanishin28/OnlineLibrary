using MediatR;
using Shared.Core.Extensions;
using Shared.Core.Models;
using ShelfContext.Domain.Entities.Base;
using ShelfContext.Domain.Entities.Shelves;
using ShelfContext.Domain.Entities.Users;
using ShelfContext.Domain.Interfaces.Queries.IsShelfNameTakenByUser;
using ShelfContext.Domain.Interfaces.Repositories;
using ShelfContext.Domain.Interfaces.Services;

namespace ShelfContext.Domain.Services
{
    public class ShelfNameCreationService : IShelfNameCreationService
    {
        private IUserRepository _userRepository;
        private IMediator _mediator;

        public ShelfNameCreationService(
            IUserRepository userRepository,
            IMediator mediator)
        {
            _userRepository = userRepository;
            _mediator = mediator;
        }

        public async Task<Result<ShelfName>> Create(UserId userId, string shelfName)
        {
            var userExists = await _userRepository.Exists(userId);

            if (!userExists)
            {
                return Result<ShelfName>.Failure(EntityErrors.NotFound);
            }

            return await CreateShelfName(userId, shelfName);
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
