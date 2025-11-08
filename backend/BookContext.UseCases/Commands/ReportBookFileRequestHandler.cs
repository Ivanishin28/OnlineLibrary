using BookContext.Contract.Commands;
using BookContext.Domain.Errors;
using BookContext.Domain.Interfaces;
using BookContext.Domain.Interfaces.Repositories;
using BookContext.Domain.ValueObjects;
using MediatR;
using Shared.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookContext.UseCases.Commands
{
    public class ReportBookFileRequestHandler : IRequestHandler<ReportBookFileRequest, Result>
    {
        private IBookMetadataRepository _bookMetadataRepository;
        private IUnitOfWork _unitOfWork;

        public ReportBookFileRequestHandler(IBookMetadataRepository bookMetadataRepository, IUnitOfWork unitOfWork)
        {
            _bookMetadataRepository = bookMetadataRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<Result> Handle(ReportBookFileRequest request, CancellationToken cancellationToken)
        {
            var bookId = new BookId(request.BookId);
            var metadata = await _bookMetadataRepository.GetBy(bookId);
            if (metadata is null)
            {
                return Result.Failure(BookMetadataErrors.NotFound(bookId));
            }
            if (metadata.FileId is null)
            {
                return Result.Failure(BookMetadataErrors.REPORTED_BOOK_WITHOUT_FILE);
            }

            metadata.SetFile(null);
            await _unitOfWork.SaveChangesAsync();

            return Result.Success();
        }
    }
}
