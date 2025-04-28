using BookContext.Contract.Commands.CreateBook;
using BookContext.DL.Repositories;
using BookContext.Domain.Entities;
using MediatR;
using Shared.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookContext.UseCases.Commands
{
    public class CreateBookRequestHandler : IRequestHandler<CreateBookRequest, Result<CreateBookResponse>>
    {
        private IBookRepository _bookRepository;

        public CreateBookRequestHandler(IBookRepository bookRepository)
        {
            _bookRepository = bookRepository;
        }

        public async Task<Result<CreateBookResponse>> Handle(CreateBookRequest request, CancellationToken cancellationToken)
        {
            var bookResult = Book.Create(request.Title, request.AuthorIds);

            if(bookResult.IsFailure)
            {
                return Result<CreateBookResponse>.Failure(bookResult.Errors.ToArray());
            }

            var book = bookResult.Model;

            await _bookRepository.Add(book);

            return new CreateBookResponse(book.Id);
        }
    }
}
