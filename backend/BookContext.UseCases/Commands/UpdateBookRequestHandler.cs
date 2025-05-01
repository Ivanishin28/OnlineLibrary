using BookContext.Contract.Commands.UpdateBook;
using BookContext.DL.Interfaces;
using BookContext.DL.Repositories;
using BookContext.Domain.ValueObjects;
using MediatR;
using Shared.Core.Models;

namespace BookContext.UseCases.Commands
{
    public class UpdateBookRequestHandler : IRequestHandler<UpdateBookRequest, Result>
    {
        private IBookRepository _bookRepository;
        private IUnitOfWork _unitOfWork;

        public UpdateBookRequestHandler(IBookRepository bookRepository, IUnitOfWork unitOfWork)
        {
            _bookRepository = bookRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<Result> Handle(UpdateBookRequest request, CancellationToken cancellationToken)
        {
            var book = await _bookRepository.GetBy(request.BookId);

            if(book is null)
            {
                throw new ArgumentException();
            }

            var authorsOfABook = AuthorsOfABook.CreateFrom(book);

            authorsOfABook.SetAuthors(request.AuthorIds);

            book.UpdateTitle(request.Title);
            var authorUpdateResult = book.UpdateAuthors(authorsOfABook);
            
            if(authorUpdateResult.IsFailure)
            {
                return authorUpdateResult;
            }

            await _unitOfWork.SaveChangesAsync();

            return Result.Success();
        }
    }
}
