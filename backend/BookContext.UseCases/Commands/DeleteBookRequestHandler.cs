using BookContext.Contract.Commands;
using BookContext.Contract.Events;
using BookContext.Domain.Errors;
using BookContext.Domain.Interfaces;
using BookContext.Domain.Interfaces.Repositories;
using MediatR;
using Shared.Core.Models;

namespace BookContext.UseCases.Commands
{
    public class DeleteBookRequestHandler : IRequestHandler<DeleteBookRequest, Result>
    {
        private IBookRepository _bookRepository;
        private IMediator _mediator;
        private IUnitOfWork _unitOfWork;

        public DeleteBookRequestHandler(
            IBookRepository bookRepository, 
            IUnitOfWork unitOfWork, 
            IMediator mediator)
        {
            _bookRepository = bookRepository;
            _unitOfWork = unitOfWork;
            _mediator = mediator;
        }

        public async Task<Result> Handle(DeleteBookRequest request, CancellationToken cancellationToken)
        {
            var book = await _bookRepository.GetBy(request.BookId);
            if (book is null)
            {
                return Result.Failure(BookErrors.NotFound(request.BookId));
            }

            _bookRepository.Delete(book);
            await _unitOfWork.SaveChangesAsync();

            var notification = new BookDeletedEvent(book.Id);
            await _mediator.Publish(notification);

            return Result.Success();
        }
    }
}
