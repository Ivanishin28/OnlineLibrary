using BookContext.Contract.Commands;
using BookContext.Domain.Entities;
using BookContext.Domain.Errors;
using BookContext.Domain.Interfaces;
using BookContext.Domain.Interfaces.Repositories;
using BookContext.Domain.ValueObjects;
using MediatR;
using Shared.Core.Models;

namespace BookContext.UseCases.Commands
{
    public class UpdateAuthorRequestHandler : IRequestHandler<UpdateAuthorRequest, Result>
    {
        private IUnitOfWork _unitOfWork;
        private IAuthorMetadataRepository _authorMetadataRepository;

        public UpdateAuthorRequestHandler(
            IUnitOfWork unitOfWork,
            IAuthorMetadataRepository authorMetadataRepository)
        {
            _unitOfWork = unitOfWork;
            _authorMetadataRepository = authorMetadataRepository;
        }

        public async Task<Result> Handle(UpdateAuthorRequest request, CancellationToken cancellationToken)
        {
            var authorId = new AuthorId(request.Id);
            var metadata = await _authorMetadataRepository.GetBy(authorId);
            if (metadata is null)
            {
                return Result.Failure(AuthorMetadataErrors.NotFound(authorId));
            }

            var updateMetadataResult = UpdateMetadata(metadata, request);
            if (updateMetadataResult.IsFailure)
            {
                return updateMetadataResult;
            }

            await _unitOfWork.SaveChangesAsync();

            return Result.Success();
        }

        private Result UpdateMetadata(AuthorMetadata metadata, UpdateAuthorRequest request)
        {
            var bioResult = AuthorBiography.Create(request.Biography);
            if (bioResult.IsFailure)
            {
                return Result.Failure(bioResult.Errors);
            }

            var avatarId = request.AvatarId is not null ? new MediaFileId(request.AvatarId.Value) : null;

            metadata.SetBirthDate(request.BirthDate);
            metadata.SetAvatar(avatarId);
            metadata.SetBiography(bioResult.Model);

            return Result.Success();
        }
    }
}
