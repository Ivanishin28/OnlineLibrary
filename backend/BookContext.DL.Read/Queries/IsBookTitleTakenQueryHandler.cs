using BookContext.Contract.Queries;
using BookContext.Domain.Interfaces.Repositories;
using MediatR;

namespace BookContext.DL.Read.Queries
{
    public class IsBookTitleTakenQueryHandler : IRequestHandler<IsBookTitleTakenQuery, bool>
    {
        private IBookRepository _bookRepository;

        public IsBookTitleTakenQueryHandler(IBookRepository bookRepository)
        {
            _bookRepository = bookRepository;
        }

        public async Task<bool> Handle(IsBookTitleTakenQuery request, CancellationToken cancellationToken)
        {
            return await _bookRepository.IsBookTitleTaken(request.Title);
        }
    }
}

