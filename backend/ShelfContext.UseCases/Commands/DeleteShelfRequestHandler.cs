using MediatR;
using Shared.Core.Models;
using ShelfContext.Contract.Commands;
using ShelfContext.Domain.Entities.Base;
using ShelfContext.Domain.Entities.Shelves;
using ShelfContext.Domain.Interfaces;
using ShelfContext.Domain.Interfaces.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShelfContext.UseCases.Commands
{
    public class DeleteShelfRequestHandler : IRequestHandler<DeleteShelfRequest, Result>
    {
        private IUnitOfWork _unitOfWork;
        private IShelfRepository _shelfRepository;

        public DeleteShelfRequestHandler(IUnitOfWork unitOfWork, IShelfRepository shelfRepository)
        {
            _unitOfWork = unitOfWork;
            _shelfRepository = shelfRepository;
        }

        public async Task<Result> Handle(DeleteShelfRequest request, CancellationToken cancellationToken)
        {
            var shelf = await _shelfRepository.GetBy(new ShelfId(request.ShelfId));
            if (shelf is null)
            {
                return Result.Failure(EntityErrors.NotFound);
            }

            _shelfRepository.Delete(shelf);

            await _unitOfWork.SaveChanges();

            return Result.Success();
        }
    }
}
