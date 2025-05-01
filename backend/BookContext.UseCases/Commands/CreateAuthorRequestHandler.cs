using BookContext.Contract.Commands.CreateAuthor;
using BookContext.DL.Interfaces;
using BookContext.DL.Repositories;
using BookContext.Domain.Entities;
using MediatR;
using Shared.Core.Models;

namespace BookContext.UseCases.Commands
{
    public class CreateAuthorRequestHandler : IRequestHandler<CreateAuthorRequest, Result<CreateAuthorResponse>>
    {
        private IAuthorRepository _authorRepository;
        private IUnitOfWork _unitOfWork;

        public CreateAuthorRequestHandler(IAuthorRepository authorRepository, IUnitOfWork unitOfWork)
        {
            _authorRepository = authorRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<Result<CreateAuthorResponse>> Handle(CreateAuthorRequest request, CancellationToken cancellationToken)
        {
            var authorResult = Author.Create(request.FirstName, request.LastName, request.BirthDate);

            if(authorResult.IsFailure)
            {
                return Result<CreateAuthorResponse>.Failure(authorResult.Errors);
            }

            var author = authorResult.Model;

            await _authorRepository.Add(author);

            await _unitOfWork.SaveChangesAsync();

            return new CreateAuthorResponse(author.Id);
        }
    }
}
