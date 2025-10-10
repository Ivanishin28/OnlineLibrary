using MediatR;
using Shared.Core.Models;
using ShelfContext.Contract.Commands.RemoveTag;
using ShelfContext.Domain.Entities.Base;
using ShelfContext.Domain.Entities.ShelvedBooks;
using ShelfContext.Domain.Entities.Tags;
using ShelfContext.Domain.Interfaces;
using ShelfContext.Domain.Interfaces.Repositories;

namespace ShelfContext.UseCases.Commands
{
    public class RemoveTagRequestHandler
        : IRequestHandler<RemoveTagRequest, Result>
    {
        private IUnitOfWork _unitOfWork;
        private IShelvedBookRepository _shelvedBookRepository;

        public RemoveTagRequestHandler(IUnitOfWork unitOfWork, IShelvedBookRepository shelvedBookRepository)
        {
            _unitOfWork = unitOfWork;
            _shelvedBookRepository = shelvedBookRepository;
        }

        public async Task<Result> Handle(RemoveTagRequest request, CancellationToken cancellationToken)
        {
            var shelvedBook = await _shelvedBookRepository.GetBy(new ShelvedBookId(request.ShelvedBookId));
            if (shelvedBook is null)
            {
                return Result.Failure(EntityErrors.NotFound);
            }

            var tagId = new TagId(request.TagId);
            var result = shelvedBook.Remove(tagId);

            if (result.IsFailure)
            {
                return result;
            }

            await _unitOfWork.SaveChanges();

            return Result.Success();
        }
    }
}
