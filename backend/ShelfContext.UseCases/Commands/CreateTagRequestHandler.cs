using MediatR;
using Shared.Core.Extensions;
using Shared.Core.Models;
using ShelfContext.Contract.Commands.CreateTag;
using ShelfContext.Domain.Entities.Tags;
using ShelfContext.Domain.Entities.Users;
using ShelfContext.Domain.Interfaces;
using ShelfContext.Domain.Interfaces.Queries;
using ShelfContext.Domain.Interfaces.Repositories;

namespace ShelfContext.UseCases.Commands
{
    public class CreateTagRequestHandler :
        IRequestHandler<CreateTagRequest, Result<Guid?>>
    {
        private ITagRepository _tagRepository;
        private ITagNameUniquenessChecker _checker;
        private IUnitOfWork _unitOfWork;

        public CreateTagRequestHandler(
            ITagRepository tagRepository, 
            ITagNameUniquenessChecker checker, 
            IUnitOfWork unitOfWork)
        {
            _tagRepository = tagRepository;
            _checker = checker;
            _unitOfWork = unitOfWork;
        }

        public async Task<Result<Guid?>> Handle(CreateTagRequest request, CancellationToken cancellationToken)
        {
            var tagResult = await CreateTag(request);

            if(tagResult.IsFailure)
            {
                return tagResult.ToFailure<Guid?>();
            }

            _tagRepository.Add(tagResult.Model);

            await _unitOfWork.SaveChanges();

            return tagResult.Model.Id.Value;
        }

        private async Task<Result<Tag>> CreateTag(CreateTagRequest request)
        {
            var nameResult = TagName.Create(request.Name);

            if(nameResult.IsFailure)
            {
                return nameResult.ToFailure<Tag>();
            }

            var userId = new UserId(request.UserId);
            if (await _checker.IsNameTakenBy(nameResult.Model, userId))
            {
                return Result<Tag>.Failure(TagErrors.NameTaken);
            }

            return Tag.Create(userId, nameResult.Model);
        }
    }
}
