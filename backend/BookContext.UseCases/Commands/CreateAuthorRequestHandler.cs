using BookContext.Contract.Commands.CreateAuthor;
using BookContext.Domain.Entities;
using BookContext.Domain.Errors;
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
        private IAuthorMetadataRepository _authorMetadataRepository;
        private IUnitOfWork _unitOfWork;

        public CreateAuthorRequestHandler(
            IAuthorRepository authorRepository, 
            IUnitOfWork unitOfWork, 
            IAuthorMetadataRepository authorMetadataRepository)
        {
            _authorRepository = authorRepository;
            _unitOfWork = unitOfWork;
            _authorMetadataRepository = authorMetadataRepository;
        }

        public async Task<Result<Guid?>> Handle(CreateAuthorRequest request, CancellationToken cancellationToken)
        {
            var author = await CreateAuthorFrom(request);
            if (author.IsFailure)
            {
                return author.ToFailure<Guid?>();
            }
            _authorRepository.Add(author.Model);

            var metadata = CreateMetadataFrom(author.Model.Id, request);
            if (metadata.IsFailure)
            {
                return metadata.ToFailure<Guid?>();
            }
            _authorMetadataRepository.Add(metadata.Model);

            await _unitOfWork.SaveChangesAsync();
            return author.Model.Id.Value;
        }

        private async Task<Result<Author>> CreateAuthorFrom(CreateAuthorRequest request)
        {
            var fullNameResult = FullName.Create(request.FirstName, request.LastName);

            if (fullNameResult.IsFailure)
            {
                return fullNameResult.ToFailure<Author>();
            }
            if (await _authorRepository.IsFullNameTaken(fullNameResult.Model))
            {
                return Result<Author>.Failure(AuthorErrors.FullNameError);
            }

            return Author.Create(
                new UserId(request.CreatorId),
                fullNameResult.Model);
        }

        private Result<AuthorMetadata> CreateMetadataFrom(AuthorId authorId, CreateAuthorRequest request)
        {
            var metadata = AuthorMetadata.Create(authorId, request.BirthDate);
            if (metadata.IsFailure)
            {
                return metadata;
            }

            if (request.AvatarId != null)
            {
                metadata.Model.SetAvatar(
                    new MediaFileId(request.AvatarId.Value));
            }

            var bio = AuthorBiography
                .Create(request.Biography);
            if (bio.IsSuccess)
            {
                metadata.Model.SetBiography(bio.Model);
            }

            return metadata;
        }
    }
}
