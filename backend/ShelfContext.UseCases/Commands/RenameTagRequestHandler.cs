using MediatR;
using Shared.Core.Models;
using ShelfContext.Contract.Commands;
using ShelfContext.Domain.Entities.Tags;
using ShelfContext.Domain.Interfaces;
using ShelfContext.Domain.Interfaces.Queries;
using ShelfContext.Domain.Interfaces.Repositories;

namespace ShelfContext.UseCases.Commands
{
    public class RenameTagRequestHandler : IRequestHandler<RenameTagRequest, Result>
    {
        private ITagRepository _tagRepository;
        private ITagNameUniquenessChecker _checker;
        private IUnitOfWork _unitOfWork;

        public RenameTagRequestHandler(
            ITagRepository tagRepository,
            IUnitOfWork unitOfWork,
            ITagNameUniquenessChecker checker)
        {
            _tagRepository = tagRepository;
            _unitOfWork = unitOfWork;
            _checker = checker;
        }

        public async Task<Result> Handle(RenameTagRequest request, CancellationToken cancellationToken)
        {
            var tagId = new TagId(request.TagId);
            var tag = await _tagRepository.GetBy(tagId);
            if (tag is null)
            {
                return Result.Failure(TagErrors.NotFound(tagId));
            }

            var tagNameResult = TagName.Create(request.Name);
            if (tagNameResult.IsFailure)
            {
                return Result.Failure(tagNameResult.Errors);
            }

            var isNameTaken = await _checker.IsNameTakenBy(tagNameResult.Model, tag.UserId);
            if (isNameTaken)
            {
                return Result.Failure(TagErrors.NameTaken);
            }

            tag.UpdateName(tagNameResult.Model);

            await _unitOfWork.SaveChanges();
            return Result.Success();
        }
    }
}

