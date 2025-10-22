using BookContext.Contract.Commands.CreateBook;
using BookContext.Domain.Entities;
using BookContext.Domain.Interfaces;
using BookContext.Domain.Interfaces.Repositories;
using BookContext.Domain.ValueObjects;
using MediatR;
using Shared.Core.Interfaces;
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
        private IUnitOfWork _unitOfWork;
        private IUserContext _context;

        public CreateBookRequestHandler(
            IBookRepository bookRepository,
            IUnitOfWork unitOfWork,
            IAuthorRepository authorRepository,
            IUserContext context)
        {
            _bookRepository = bookRepository;
            _unitOfWork = unitOfWork;
            _context = context;
        }

        public async Task<Result<CreateBookResponse>> Handle(CreateBookRequest request, CancellationToken cancellationToken)
        {
            var userId = new UserId(_context.UserId);
            var bookResult = Book.Create(userId, request.Title);

            if(bookResult.IsFailure)
            {
                return Result<CreateBookResponse>.Failure(bookResult.Errors);
            }

            var book = bookResult.Model;

            await AddBook(book);

            return new CreateBookResponse(book.Id.Value);
        }

        private async Task AddBook(Book book)
        {
            _bookRepository.Add(book);
            await _unitOfWork.SaveChangesAsync();
        }
    }
}
