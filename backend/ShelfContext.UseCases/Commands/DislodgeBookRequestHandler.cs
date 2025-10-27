using MediatR;
using Shared.Core.Models;
using ShelfContext.Contract.Commands.DislodgeBook;
using ShelfContext.Contract.Events;
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
        private IMediator _mediator;

        public DislodgeBookRequestHandler(IUnitOfWork unitOfWork, IShelvedBookRepository shelvedBookRepository, IMediator mediator)
        {
            _unitOfWork = unitOfWork;
            _shelvedBookRepository = shelvedBookRepository;
            _mediator = mediator;
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

            await _mediator.Send(new BookDislodgedEvent()
            {
                BookId = shelvedBook.BookId.Value,
                UserId = shelvedBook.UserId.Value,
                ShelvedBookId = shelvedBook.Id.Value
            });

            return Result.Success();
        }
    }
}
