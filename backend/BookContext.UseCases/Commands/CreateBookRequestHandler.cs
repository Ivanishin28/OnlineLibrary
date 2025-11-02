using BookContext.Contract.Commands.CreateBook;
using BookContext.Domain.Entities;
using BookContext.Domain.Interfaces;
using BookContext.Domain.Interfaces.Repositories;
using BookContext.Domain.ValueObjects;
using MediatR;
using Shared.Core.Extensions;
using Shared.Core.Models;

namespace BookContext.UseCases.Commands
{
    public class CreateBookRequestHandler : IRequestHandler<CreateBookRequest, Result<Guid?>>
    {
        private IBookRepository _bookRepository;
        private IBookMetadataRepository _bookMetadataRepository;
        private IAuthorRepository _authorRepository;
        private IUnitOfWork _unitOfWork;

        public CreateBookRequestHandler(
            IBookRepository bookRepository,
            IBookMetadataRepository bookMetadataRepository,
            IUnitOfWork unitOfWork,
            IAuthorRepository authorRepository)
        {
            _bookRepository = bookRepository;
            _bookMetadataRepository = bookMetadataRepository;
            _unitOfWork = unitOfWork;
            _authorRepository = authorRepository;
        }

        public async Task<Result<Guid?>> Handle(CreateBookRequest request, CancellationToken cancellationToken)
        {
            var bookResult = await CreateBookFrom(request);
            if(bookResult.IsFailure)
            {
                return bookResult.ToFailure<Guid?>();
            }

            var book = bookResult.Model;

            var metadataResult = CreateMetadataFrom(book.Id, request);
            if (metadataResult.IsFailure)
            {
                return metadataResult.ToFailure<Guid?>();
            }

            _bookRepository.Add(book);
            _bookMetadataRepository.Add(metadataResult.Model);

            await _unitOfWork.SaveChangesAsync();

            return book.Id.Value;
        }

        private async Task<Result<Book>> CreateBookFrom(CreateBookRequest request)
        {
            var idsToSet = request
                .AuthorIds
                .Select(x => new AuthorId(x)).ToList();
            var existingIds = await _authorRepository.EnsureExist(idsToSet);

            var userId = new UserId(request.CreatorId);
            return Book.Create(userId, request.Title, existingIds);
        }

        private Result<BookMetadata> CreateMetadataFrom(BookId bookId, CreateBookRequest request)
        {
            var descResult = BookDescription.Create(request.Description);
            var coverId = request.CoverId is not null ?
                new MediaFileId(request.CoverId.Value) :
                null;
            return new BookMetadata(bookId, request.PublishingDate, coverId, descResult.Model);
        }
    }
}
