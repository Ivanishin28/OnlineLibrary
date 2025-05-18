using Shared.Core.Extensions;
using Shared.Core.Models;
using ShelfContext.Domain.DTOs;
using ShelfContext.Domain.Entities.Shelves;
using ShelfContext.Domain.Entities.Users;
using ShelfContext.Domain.Interfaces.Repositories;
using ShelfContext.Domain.Interfaces.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShelfContext.Domain.Services
{
    public class ShelfCreationService : IShelfCreationService
    {
        private IShelfRepository _shelfRepository;

        public ShelfCreationService(IShelfRepository shelfRepository)
        {
            _shelfRepository = shelfRepository;
        }

        public async Task<Result<Shelf>> Create(UserId userId, ShelfDto dto)
        {
            var name = ShelfName.Create(dto.Name);

            if (name.IsFailure)
            {
                return name.ToFailure<Shelf>();
            }

            var userHasShelf = await _shelfRepository.IsNameUniqueForUser(name.Model, userId);

            if (userHasShelf)
            {
                return Result<Shelf>.Failure(ShelfErrors.DuplicateName);
            }

            return Shelf.Create(userId, name.Model);
        }
    }
}
