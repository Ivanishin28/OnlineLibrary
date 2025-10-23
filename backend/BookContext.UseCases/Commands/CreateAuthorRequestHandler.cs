using BookContext.Contract.Commands.CreateAuthor;
using BookContext.Domain.Entities;
using BookContext.Domain.Interfaces;
using BookContext.Domain.Interfaces.Repositories;
using BookContext.Domain.ValueObjects;
using MediatR;
using Shared.Core.Extensions;
using Shared.Core.Models;

namespace BookContext.UseCases.Commands
{
    public class CreateAuthorRequestHandler : IRequestHandler<CreateAuthorRequest, Result<Guid?>>
    {
        private IAuthorRepository _authorRepository;
        private IUnitOfWork _unitOfWork;

        public CreateAuthorRequestHandler(IAuthorRepository authorRepository, IUnitOfWork unitOfWork)
        {
            _authorRepository = authorRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<Result<Guid?>> Handle(CreateAuthorRequest request, CancellationToken cancellationToken)
        {
            var fullNameResult = FullName.Create(request.FirstName, request.LastName);

            if(fullNameResult.IsFailure)
            {
                return fullNameResult.ToFailure<Guid?>();
            }

            var authorResult = Author.Create(
                new UserId(request.CreatorId), 
                fullNameResult.Model, 
                request.BirthDate);

            if(authorResult.IsFailure)
            {
                return Result<Guid?>.Failure(authorResult.Errors);
            }

            var author = authorResult.Model;

            await _authorRepository.Add(author);

            await _unitOfWork.SaveChangesAsync();

            return author.Id.Value;
        }
    }
}
