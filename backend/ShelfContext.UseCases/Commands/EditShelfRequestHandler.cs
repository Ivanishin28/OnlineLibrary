using MediatR;
using Shared.Core.Models;
using ShelfContext.Contract.Commands.EditShelf;
using ShelfContext.Domain.DTOs;
using ShelfContext.Domain.Entities.Shelves;
using ShelfContext.Domain.Interfaces;
using ShelfContext.Domain.Interfaces.Services;

namespace ShelfContext.UseCases.Commands
{
    public class EditShelfRequestHandler
        : IRequestHandler<EditShelfRequest, Result>
    {
        private IUnitOfWork _unitOfWork;
        private IShelfCreationService _shelfCreationService;

        public EditShelfRequestHandler(IUnitOfWork unitOfWork, IShelfCreationService shelfCreationService)
        {
            _unitOfWork = unitOfWork;
            _shelfCreationService = shelfCreationService;
        }

        public async Task<Result> Handle(EditShelfRequest request, CancellationToken cancellationToken)
        {
            var shelfId = new ShelfId(request.ShelfId);
            var shelfDto = new ShelfDto(request.Name);

            var result = await _shelfCreationService.Update(shelfId, shelfDto);

            if(result.IsFailure)
            {
                return result;
            }

            await _unitOfWork.SaveChanges();

            return Result.Success();
        }
    }
}
