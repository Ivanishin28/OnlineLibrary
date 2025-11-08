using BookContext.Contract.Commands;
using BookContext.Domain.Errors;
using BookContext.Domain.Interfaces;
using BookContext.Domain.Interfaces.Repositories;
using BookContext.Domain.ValueObjects;
using MediatR;
using Shared.Core.Models;

namespace BookContext.UseCases.Commands
{
    public class DeleteAuthorRequestHandler : IRequestHandler<DeleteAuthorRequest, Result>
    {
        private IAuthorRepository _authorRepository;
        private IUnitOfWork _unitOfWork;

        public DeleteAuthorRequestHandler(
            IAuthorRepository authorRepository, 
            IUnitOfWork unitOfWork)
        {
            _authorRepository = authorRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<Result> Handle(DeleteAuthorRequest request, CancellationToken cancellationToken)
        {
            var authorId = new AuthorId(request.AuthorId);
            var author = await _authorRepository.GetBy(authorId);
            if (author is null)
            {
                return Result.Failure(AuthorErrors.NotFound(authorId));
            }

            _authorRepository.Remove(author);
            await _unitOfWork.SaveChangesAsync();

            return Result.Success();
        }
    }
}
