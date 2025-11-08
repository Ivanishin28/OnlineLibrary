using BookContext.Contract.Commands;
using BookContext.Domain.Entities;
using BookContext.Domain.Errors;
using BookContext.Domain.Interfaces;
using BookContext.Domain.Interfaces.Repositories;
using BookContext.Domain.ValueObjects;
using MediatR;
using Shared.Core.Extensions;
using Shared.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookContext.UseCases.Commands
{
    public class UpdateBookRequestHandler : IRequestHandler<UpdateBookRequest, Result>
    {
        private IUnitOfWork _unitOfWork;
        private IBookRepository _bookRepository;
        private IBookMetadataRepository _bookMetadataRepository;
        private IAuthorRepository _authorRepository;
        private IGenreRepository _genreRepository;

        public UpdateBookRequestHandler(
            IUnitOfWork unitOfWork,
            IBookRepository bookRepository,
            IBookMetadataRepository bookMetadataRepository,
            IAuthorRepository authorRepository,
            IGenreRepository genreRepository)
        {
            _unitOfWork = unitOfWork;
            _bookRepository = bookRepository;
            _bookMetadataRepository = bookMetadataRepository;
            _authorRepository = authorRepository;
            _genreRepository = genreRepository;
        }

        public async Task<Result> Handle(UpdateBookRequest request, CancellationToken cancellationToken)
        {
            var bookId = new BookId(request.Id);
            var book = await _bookRepository.GetBy(bookId);
            if (book is null)
            {
                return Result.Failure(BookErrors.NotFound(bookId.Value));
            }

            var metadata = await _bookMetadataRepository.GetBy(bookId);
            if (metadata is null)
            {
                return Result.Failure(BookMetadataErrors.NotFound(bookId));
            }

            var updateMetadataResult = UpdateMetadata(metadata, request);
            if (updateMetadataResult.IsFailure)
            {
                return updateMetadataResult;
            }

            var updateAuthorsResult = await UpdateAuthors(book, request.AuthorIds);
            if (updateAuthorsResult.IsFailure)
            {
                return updateAuthorsResult;
            }

            var updateGenresResult = await UpdateGenres(book, request.GenreIds);
            if (updateGenresResult.IsFailure)
            {
                return updateGenresResult;
            }

            await _unitOfWork.SaveChangesAsync();

            return Result.Success();
        }

        private async Task<Result> UpdateAuthors(Book book, ICollection<Guid> authorIds)
        {
            var ids = authorIds.Select(x => new AuthorId(x)).ToList();
            var existingIds = await _authorRepository.EnsureExist(ids);

            var currentAuthorIds = book.BookAuthors.Select(ba => ba.AuthorId).ToList();
            var authorsToRemove = currentAuthorIds.Except(existingIds).ToList();
            var authorsToAdd = existingIds.Except(currentAuthorIds).ToList();

            foreach(var authorId in authorsToRemove)
            {
                var removeResult = book.RemoveAuthor(authorId);
                if (removeResult.IsFailure)
                {
                    return removeResult;
                }
            }

            foreach(var authorId in authorsToAdd)
            {
                var addResult = book.AddAuthor(authorId);
                if (addResult.IsFailure)
                {
                    return addResult;
                }
            }

            return Result.Success();
        }

        private async Task<Result> UpdateGenres(Book book, ICollection<Guid> genreIds)
        {
            var ids = genreIds.Select(x => new GenreId(x)).ToList();
            var existingIds = await _genreRepository.EnsureExist(ids);

            var currentGenreIds = book.BookGenres.Select(bg => bg.GenreId).ToList();
            var genresToRemove = currentGenreIds.Except(existingIds).ToList();
            var genresToAdd = existingIds.Except(currentGenreIds).ToList();

            foreach(var genreId in genresToRemove)
            {
                var removeResult = book.RemoveGenre(genreId);
                if (removeResult.IsFailure)
                {
                    return removeResult;
                }
            }

            foreach(var genreId in genresToAdd)
            {
                var addResult = book.AddGenre(genreId);
                if (addResult.IsFailure)
                {
                    return addResult;
                }
            }

            return Result.Success();
        }

        private Result UpdateMetadata(BookMetadata metadata, UpdateBookRequest request)
        {
            var descResult = BookDescription.Create(request.Description);
            if (descResult.IsFailure)
            {
                return descResult;
            }

            var coverId = request.CoverId is not null ? new MediaFileId(request.CoverId.Value) : null;
            var fileId = request.FileId is not null ? new MediaFileId(request.FileId.Value) : null;

            metadata.SetPublishingDate(request.PublishingDate);
            metadata.SetCover(coverId);
            metadata.SetFile(fileId);
            metadata.SetDescription(descResult.Model);

            return Result.Success();
        }
    }
}
