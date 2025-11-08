using BookContext.Contract.Queries;
using BookContext.Domain.Interfaces.Repositories;
using BookContext.Domain.ValueObjects;
using MediatR;
using Shared.Core.Extensions;

namespace BookContext.DL.Read.Queries
{
    public class IsAuthorFullNameTakenQueryHandler : IRequestHandler<IsAuthorFullNameTakenQuery, bool>
    {
        private IAuthorRepository _authorRepository;

        public IsAuthorFullNameTakenQueryHandler(IAuthorRepository authorRepository)
        {
            _authorRepository = authorRepository;
        }

        public async Task<bool> Handle(IsAuthorFullNameTakenQuery request, CancellationToken cancellationToken)
        {
            var fullNameResult = FullName.Create(request.FirstName, request.LastName, request.MiddleName);

            if (fullNameResult.IsFailure)
            {
                return false;
            }

            return await _authorRepository.IsFullNameTaken(fullNameResult.Model);
        }
    }
}

