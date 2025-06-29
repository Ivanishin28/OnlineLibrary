using MediatR;
using Shared.Core.Models;
using ShelfContext.Contract.Commands.EditShelf;
using ShelfContext.Domain.Entities.Shelves;
using ShelfContext.Domain.Interfaces;
using ShelfContext.Domain.Interfaces.Services;

namespace ShelfContext.UseCases.Commands
{
    public class EditShelfRequestHandler
        : IRequestHandler<EditShelfRequest, Result>
    {
        private IUnitOfWork _unitOfWork;
        private IShelfNameCreationService _shelfCreationService;

        public EditShelfRequestHandler(IUnitOfWork unitOfWork, IShelfNameCreationService shelfCreationService)
        {
            _unitOfWork = unitOfWork;
            _shelfCreationService = shelfCreationService;
        }

        public async Task<Result> Handle(EditShelfRequest request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
