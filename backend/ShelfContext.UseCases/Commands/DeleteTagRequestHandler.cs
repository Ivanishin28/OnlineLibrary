using MediatR;
using Shared.Core.Models;
using ShelfContext.Contract.Commands;
using ShelfContext.Domain.Entities.Base;
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
    public class DeleteTagRequestHandler : IRequestHandler<DeleteTagRequest, Result>
    {
        private IUnitOfWork _unitOfWork;
        private ITagRepository _tagRepository;

        public DeleteTagRequestHandler(IUnitOfWork unitOfWork, ITagRepository tagRepository)
        {
            _unitOfWork = unitOfWork;
            _tagRepository = tagRepository;
        }

        public async Task<Result> Handle(DeleteTagRequest request, CancellationToken cancellationToken)
        {
            var tag = await _tagRepository.GetBy(new TagId(request.TagId));
            if (tag is null)
            {
                return Result.Failure(EntityErrors.NotFound);
            }

            _tagRepository.Remove(tag);

            await _unitOfWork.SaveChanges();

            return Result.Success();
        }
    }
}
