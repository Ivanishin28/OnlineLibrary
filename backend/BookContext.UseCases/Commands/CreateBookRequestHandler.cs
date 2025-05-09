using BookContext.Contract.Commands.CreateBook;
using BookContext.DL.Interfaces;
using BookContext.DL.Repositories;
using BookContext.Domain.Entities;
using MediatR;
using Shared.Core.Models;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookContext.UseCases.Commands
{
    public class CreateBookRequestHandler : IRequestHandler<CreateBookRequest, Result<CreateBookResponse>>
    {
        private IBookRepository _bookRepository;
        private IAuthorRepository _authorRepository;
        private IUnitOfWork _unitOfWork;

        public CreateBookRequestHandler(IBookRepository bookRepository, IUnitOfWork unitOfWork, IAuthorRepository authorRepository)
        {
            _bookRepository = bookRepository;
            _unitOfWork = unitOfWork;
            _authorRepository = authorRepository;
        }

        public async Task<Result<CreateBookResponse>> Handle(CreateBookRequest request, CancellationToken cancellationToken)
        {
            var authorIds = await GetExistingAuthorIds(request.AuthorIds);

            var bookResult = Book.Create(request.Title, authorIds);

            if(bookResult.IsFailure)
            {
                return Result<CreateBookResponse>.Failure(bookResult.Errors);
            }

            var book = bookResult.Model;

            await AddBook(book);

            return new CreateBookResponse(book.Id);
        }

        private async Task<ICollection<Guid>> GetExistingAuthorIds(IEnumerable<Guid> authorIds)
        {
            var authors = await _authorRepository.GetByIds(authorIds);

            return authors
                .Select(author => author.Id)
                .ToList();
        }

        private async Task AddBook(Book book)
        {
            await _bookRepository.Add(book);
            await _unitOfWork.SaveChangesAsync();
        }
    }
}
