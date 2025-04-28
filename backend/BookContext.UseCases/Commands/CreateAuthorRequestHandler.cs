using BookContext.Contract.Commands.CreateAuthor;
using BookContext.DL.Repositories;
using BookContext.Domain.Entities;
using MediatR;
using Shared.Core.Models;

namespace BookContext.UseCases.Commands
{
    public class CreateAuthorRequestHandler : IRequestHandler<CreateAuthorRequest, Result<CreateAuthorResponse>>
    {
        private IAuthorRepository _authorRepository;

        public CreateAuthorRequestHandler(IAuthorRepository authorRepository)
        {
            _authorRepository = authorRepository;
        }

        public async Task<Result<CreateAuthorResponse>> Handle(CreateAuthorRequest request, CancellationToken cancellationToken)
        {
            var authorResult = Author.Create(request.FirstName, request.LastName, request.BirthDate);

            if(authorResult.IsFailure)
            {
                return Result<CreateAuthorResponse>.Failure(authorResult.Errors.ToArray());
            }

            var author = authorResult.Model;

            await _authorRepository.Add(author);
            return new CreateAuthorResponse(author.Id);
        }
    }
}
