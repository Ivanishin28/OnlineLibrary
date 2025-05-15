using MediatR;
using Shared.Core.Extensions;
using Shared.Core.Models;
using ShelfContext.Contract.Commands.CreateTag;
using ShelfContext.Domain.Entities.Tags;
using ShelfContext.Domain.Interfaces;
using ShelfContext.Domain.Interfaces.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShelfContext.UseCases.Commands
{
    public class CreateTagRequestHandler :
        IRequestHandler<CreateTagRequest, Result<CreateTagResponse>>
    {
        private ITagRepository _tagRepository;
        private IUnitOfWork _unitOfWork;

        public CreateTagRequestHandler(ITagRepository tagRepository, IUnitOfWork unitOfWork)
        {
            _tagRepository = tagRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<Result<CreateTagResponse>> Handle(CreateTagRequest request, CancellationToken cancellationToken)
        {
            var tagResult = CreateTag(request);

            if(tagResult.IsFailure)
            {
                return tagResult.ToFailure<CreateTagResponse>();
            }

            await AddTag(tagResult.Model);

            return new CreateTagResponse(tagResult.Model.Id.Value);
        }

        private Result<Tag> CreateTag(CreateTagRequest request)
        {
            var nameResult = TagName.Create(request.Name);

            if(nameResult.IsFailure)
            {
                return nameResult.ToFailure<Tag>();
            }

            return Tag.Create(nameResult.Model);
        }

        private async Task AddTag(Tag tag)
        {
            await _tagRepository.Add(tag);
            await _unitOfWork.SaveChanges();
        }
    }
}
