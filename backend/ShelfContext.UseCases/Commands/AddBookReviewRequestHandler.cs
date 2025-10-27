using MediatR;
using Shared.Core.Extensions;
using Shared.Core.Models;
using ShelfContext.Contract.Commands;
using ShelfContext.Domain.Entities.Books;
using ShelfContext.Domain.Entities.Review;
using ShelfContext.Domain.Entities.Users;
using ShelfContext.Domain.Interfaces;
using ShelfContext.Domain.Interfaces.Repositories;

namespace ShelfContext.UseCases.Commands
{
    public class AddBookReviewRequestHandler : IRequestHandler<AddBookReviewRequest, Result<Guid?>>
    {
        private IUnitOfWork _unitOfWork;
        private IShelvedBookRepository _shelvedBookRepository;
        private IReviewRepository _reviewRepository;

        public AddBookReviewRequestHandler(
            IUnitOfWork unitOfWork, 
            IShelvedBookRepository shelvedBookRepository, 
            IReviewRepository reviewRepository)
        {
            _unitOfWork = unitOfWork;
            _shelvedBookRepository = shelvedBookRepository;
            _reviewRepository = reviewRepository;
        }

        public async Task<Result<Guid?>> Handle(AddBookReviewRequest request, CancellationToken cancellationToken)
        {
            var userId = new UserId(request.UserId);
            var bookId = new BookId(request.BookId);
            if (!await _shelvedBookRepository.Exists(userId, bookId))
            {
                return Result<Guid?>.Failure(ReviewErrors.REVIEW_ONLY_SHELVED_BOOKS);
            }
            else if (await _reviewRepository.Exists(userId, bookId))
            {
                return Result<Guid?>.Failure(ReviewErrors.USER_ALREADY_REVIEWED);
            }

            var review = CreateReview(userId, bookId, request);
            if (review.IsFailure)
            {
                return review.ToFailure<Guid?>();
            }

            _reviewRepository.Add(review.Model);
            await _unitOfWork.SaveChanges();

            return Guid.NewGuid();
        }

        private Result<Review> CreateReview(UserId userId, BookId bookId, AddBookReviewRequest request)
        {
            var rating = Rating.Create(request.Rating);
            var text = ReviewText.Create(request.Text);
            var result = ResultExtensions.Combine(rating, text);
            if (result.IsFailure)
            {
                return result.ToFailure<Review>();
            }

            return new Review(userId, bookId, text.Model, rating.Model, DateTime.Now);
        }
    }
}
