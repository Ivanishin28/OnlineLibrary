using BookContext.Contract.Commands.UpdateBook;
using BookContext.DL.Repositories;
using MediatR;
using Shared.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookContext.UseCases.Commands
{
    public class UpdateBookRequestHandler : IRequestHandler<UpdateBookRequest, Result>
    {
        private IBookRepository _bookRepository;

        public UpdateBookRequestHandler(IBookRepository bookRepository)
        {
            _bookRepository = bookRepository;
        }

        public Task<Result> Handle(UpdateBookRequest request, CancellationToken cancellationToken)
        {
            var book = _bookRepository.GetBy(request.BookId);

            if(book is null)
            {
                throw new ArgumentException();
            }

            throw new NotImplementedException();
        }
    }
}
