using MediatR;
using Shared.Core.Extensions;
using Shared.Core.Models;
using ShelfContext.Contract.Commands.AddTagToBook;
using ShelfContext.Contract.Errors;
using ShelfContext.Contract.Services;
using ShelfContext.Domain.Entities.Base;
using ShelfContext.Domain.Entities.ShelvedBooks;
using ShelfContext.Domain.Entities.Tags;
using ShelfContext.Domain.Entities.Users;
using ShelfContext.Domain.Interfaces;
using ShelfContext.Domain.Interfaces.Repositories;

namespace ShelfContext.UseCases.Commands
{
    public class AddTagToBookRequestHandler
        : IRequestHandler<AddTagToBookRequest, Result<Guid?>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IShelvedBookRepository _shelvedBookRepository;
        private readonly ITagRepository _tagRepository;
        private readonly IResouceAccessibilityChecker _checker;

        public AddTagToBookRequestHandler(
            IUnitOfWork unitOfWork,
            IShelvedBookRepository shelvedBookRepository,
            ITagRepository tagRepository,
            IResouceAccessibilityChecker checker)
        {
            _unitOfWork = unitOfWork;
            _shelvedBookRepository = shelvedBookRepository;
            _tagRepository = tagRepository;
            _checker = checker;
        }

        public async Task<Result<Guid?>> Handle(AddTagToBookRequest request, CancellationToken cancellationToken)
        {
            var tagId = new TagId(request.TagId);
            var shelvedBookId = new ShelvedBookId(request.ShelvedBookId);
            var userId = new UserId(request.UserId);

            var canAccess = await CanAccess(tagId, shelvedBookId, userId);
            if (canAccess.IsFailure)
            {
                return canAccess.ToFailure<Guid?>();
            }

            var tag = await _tagRepository.GetBy(tagId);
            var shelvedBook = await _shelvedBookRepository.GetBy(shelvedBookId);

            if (tag is null || shelvedBook is null)
            {
                return Result<Guid?>.Failure(EntityErrors.NotFound);
            }

            var result = shelvedBook.Add(tag);

            if (result.IsFailure)
            {
                return result.ToFailure<Guid?>();
            }

            await _unitOfWork.SaveChanges();

            return result.Model.Value;
        }

        private async Task<Result> CanAccess(TagId tagId, ShelvedBookId shelvedBookId, UserId userId)
        {
            if (!(await _checker.IsTagAccessibleToUser(tagId.Value, userId.Value)))
            {
                return Result.Failure(AccessibilityErrors.CannotAccessTag(userId.Value, tagId.Value));
            }
            else if (!(await _checker.IsShelvedBookAccessibleToUser(shelvedBookId.Value, userId.Value)))
            {
                return Result.Failure(AccessibilityErrors.CannotAccessTag(userId.Value, tagId.Value));
            }
            else
            {
                return Result.Success();
            }
        }
    }
}
