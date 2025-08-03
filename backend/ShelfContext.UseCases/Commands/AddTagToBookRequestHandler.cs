using MediatR;
using Shared.Core.Models;
using ShelfContext.Contract.Commands.AddTagToBook;
using ShelfContext.Domain.Entities.Base;
using ShelfContext.Domain.Entities.ShelvedBooks;
using ShelfContext.Domain.Entities.Tags;
using ShelfContext.Domain.Interfaces;
using ShelfContext.Domain.Interfaces.Repositories;

namespace ShelfContext.UseCases.Commands
{
    public class AddTagToBookRequestHandler
        : IRequestHandler<AddTagToBookRequest, Result>
    {
        private IUnitOfWork _unitOfWork;
        private IShelvedBookRepository _shelvedBookRepository;
        private ITagRepository _tagRepository;

        public AddTagToBookRequestHandler(
            IUnitOfWork unitOfWork, 
            IShelvedBookRepository shelvedBookRepository, 
            ITagRepository tagRepository)
        {
            _unitOfWork = unitOfWork;
            _shelvedBookRepository = shelvedBookRepository;
            _tagRepository = tagRepository;
        }

        public async Task<Result> Handle(AddTagToBookRequest request, CancellationToken cancellationToken)
        {
            var tagId = new TagId(request.TagId);
            var tag = await _tagRepository.GetBy(tagId);

            if (tag is null)
            {
                return Result.Failure(EntityErrors.NotFound);
            }

            var shelvedBookId = new ShelvedBookId(request.ShelvedBookId);
            var shelvedBook = await _shelvedBookRepository.GetBy(shelvedBookId);

            if (shelvedBook is null)
            {
                return Result.Failure(EntityErrors.NotFound);
            }

            shelvedBook.Add(tag);

            await _unitOfWork.SaveChanges();

            return Result.Success();
        }
    }
}
