using MediatR;
using Shared.Core.Models;
using ShelfContext.Contract.Commands.DislodgeBook;
using ShelfContext.Domain.Entities.Base;
using ShelfContext.Domain.Entities.ShelvedBooks;
using ShelfContext.Domain.Interfaces;
using ShelfContext.Domain.Interfaces.Repositories;

namespace ShelfContext.UseCases.Commands
{
    public class DislodgeBookRequestHandler
        : IRequestHandler<DislodgeBookRequest, Result>
    {
        private IUnitOfWork _unitOfWork;
        private IShelvedBookRepository _shelvedBookRepository;

        public DislodgeBookRequestHandler(IUnitOfWork unitOfWork, IShelvedBookRepository shelvedBookRepository)
        {
            _unitOfWork = unitOfWork;
            _shelvedBookRepository = shelvedBookRepository;
        }

        public async Task<Result> Handle(DislodgeBookRequest request, CancellationToken cancellationToken)
        {
            var id = new ShelvedBookId(request.ShelvedBookId);
            var shelvedBook = await _shelvedBookRepository.GetBy(id);

            if (shelvedBook is null)
            {
                return Result.Failure(EntityErrors.NotFound);
            }

            _shelvedBookRepository.Remove(shelvedBook);

            await _unitOfWork.SaveChanges();

            return Result.Success();
        }
    }
}
