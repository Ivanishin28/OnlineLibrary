using BookContext.Contract.Commands.CreateBook;
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
    public class CreateBookRequestHandler : IRequestHandler<CreateBookRequest, Result<Guid?>>
    {
        private IBookRepository _bookRepository;
        private IBookMetadataRepository _bookMetadataRepository;
        private IAuthorRepository _authorRepository;
        private IGenreRepository _genreRepository;
        private IUnitOfWork _unitOfWork;

        public CreateBookRequestHandler(
            IBookRepository bookRepository,
            IBookMetadataRepository bookMetadataRepository,
            IUnitOfWork unitOfWork,
            IAuthorRepository authorRepository,
            IGenreRepository genreRepository)
        {
            _bookRepository = bookRepository;
            _bookMetadataRepository = bookMetadataRepository;
            _unitOfWork = unitOfWork;
            _authorRepository = authorRepository;
            _genreRepository = genreRepository;
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
            var authorIdsToSet = request
                .AuthorIds
                .Select(x => new AuthorId(x)).ToList();
            var existingAuthorIds = await _authorRepository.EnsureExist(authorIdsToSet);

            if (await _bookRepository.IsBookTitleTaken(request.Title))
            {
                return Result<Book>.Failure(BookErrors.BookTitleTaken(request.Title));
            }

            var genreIdsToSet = request
                .GenreIds
                .Select(x => new GenreId(x)).ToList();
            var existingGenreIds = await _genreRepository.EnsureExist(genreIdsToSet);

            var userId = new UserId(request.CreatorId);
            return Book.Create(userId, request.Title, existingAuthorIds, existingGenreIds);
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
