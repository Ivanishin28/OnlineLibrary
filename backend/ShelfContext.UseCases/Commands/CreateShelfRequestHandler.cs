using MediatR;
using Shared.Core.Extensions;
using Shared.Core.Models;
using ShelfContext.Contract.Commands.CreateShelf;
using ShelfContext.Domain.Entities.Shelves;
using ShelfContext.Domain.Entities.Users;
using ShelfContext.Domain.Interfaces;
using ShelfContext.Domain.Interfaces.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShelfContext.UseCases.Commands
{
    public class CreateShelfRequestHandler :
        IRequestHandler<CreateShelfRequest, Result<CreateShelfResponse>>
    {
        private IShelfRepository _shelfRepository;
        private IUnitOfWork _unitOfWork;
        public CreateShelfRequestHandler(IShelfRepository shelfRepository, IUnitOfWork unitOfWork)
        {
            _shelfRepository = shelfRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<Result<CreateShelfResponse>> Handle(CreateShelfRequest request, CancellationToken cancellationToken)
        {
            var shelfResult = CreateShelf(request);

            if(shelfResult.IsFailure)
            {
                return shelfResult.ToFailure<CreateShelfResponse>();
            }

            await AddShelf(shelfResult.Model);

            return new CreateShelfResponse(shelfResult.Model.Id.Value);
        }

        private Result<Shelf> CreateShelf(CreateShelfRequest request)
        {
            var name = ShelfName.Create(request.Name);
            var userId = new UserId(request.ClientId);

            if (name.IsFailure)
            {
                return name.ToFailure<Shelf>();
            }

            return Shelf.Create(userId, name.Model);
        }

        private async Task AddShelf(Shelf shelf)
        {
            await _shelfRepository.Add(shelf);

            await _unitOfWork.SaveChanges();
        }
    }
}
