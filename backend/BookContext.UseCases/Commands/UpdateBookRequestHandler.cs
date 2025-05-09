using BookContext.Contract.Commands.UpdateBook;
using BookContext.DL.Interfaces;
using BookContext.DL.Repositories;
using BookContext.Domain.Entities;
using BookContext.Domain.ValueObjects;
using MediatR;
using Shared.Core.Models;
using Shared.DL.Errors;

namespace BookContext.UseCases.Commands
{
    public class UpdateBookRequestHandler : IRequestHandler<UpdateBookRequest, Result>
    {
        private IBookRepository _bookRepository;
        private IAuthorRepository _authorRepository;
        private IUnitOfWork _unitOfWork;

        public UpdateBookRequestHandler(
            IBookRepository bookRepository,
            IUnitOfWork unitOfWork,
            IAuthorRepository authorRepository)
        {
            _bookRepository = bookRepository;
            _unitOfWork = unitOfWork;
            _authorRepository = authorRepository;
        }

        public async Task<Result> Handle(UpdateBookRequest request, CancellationToken cancellationToken)
        {
            var book = await _bookRepository.GetBy(request.BookId);

            if(book is null)
            {
                return Result.Failure(DataAccessErrors.NotFound);
            }
            
            var authorIds = await GetExistingAuthorIds(request.AuthorIds);

            var authorsOfABook = AuthorsOfABook.CreateFrom(book);

            authorsOfABook.SetAuthors(authorIds);

            book.UpdateTitle(request.Title);
            var authorUpdateResult = book.UpdateAuthors(authorsOfABook);
            
            if(authorUpdateResult.IsFailure)
            {
                return authorUpdateResult;
            }

            await _unitOfWork.SaveChangesAsync();

            return Result.Success();
        }

        private async Task<ICollection<Guid>> GetExistingAuthorIds(IEnumerable<Guid> authorIds)
        {
            var authors = await _authorRepository.GetByIds(authorIds);

            return authors
                .Select(author => author.Id)
                .ToList();
        }
    }
}
