using BookContext.Contract.Commands.CreateBook;
using BookContext.Domain.Entities;
using BookContext.Domain.Interfaces;
using BookContext.Domain.Interfaces.Repositories;
using BookContext.Domain.ValueObjects;
using MediatR;
using Shared.Core.Extensions;
using Shared.Core.Interfaces;
using Shared.Core.Models;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookContext.UseCases.Commands
{
    public class CreateBookRequestHandler : IRequestHandler<CreateBookRequest, Result<Guid?>>
    {
        private IBookRepository _bookRepository;
        private IBookMetadataRepository _bookMetadataRepository;
        private IUnitOfWork _unitOfWork;

        public CreateBookRequestHandler(
            IBookRepository bookRepository,
            IBookMetadataRepository bookMetadataRepository,
            IUnitOfWork unitOfWork)
        {
            _bookRepository = bookRepository;
            _bookMetadataRepository = bookMetadataRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<Result<Guid?>> Handle(CreateBookRequest request, CancellationToken cancellationToken)
        {
            var userId = new UserId(request.CreatorId);
            var bookResult = Book.Create(userId, request.Title);

            if(bookResult.IsFailure)
            {
                return Result<Guid?>.Failure(bookResult.Errors);
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
