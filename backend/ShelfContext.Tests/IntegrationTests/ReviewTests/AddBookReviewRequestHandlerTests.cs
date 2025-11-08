using Microsoft.EntityFrameworkCore;
using ShelfContext.DL.SqlServer.Repositories;
using ShelfContext.DL.SqlServer;
using ShelfContext.UseCases.EventHandlers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ShelfContext.UseCases.Commands;
using ShelfContext.DL.SqlServer.Concrete;
using ShelfContext.Domain.Entities.Users;
using ShelfContext.Domain.Entities.Books;
using ShelfContext.Contract.Commands;
using ShelfContext.Domain.Entities.Review;
using ShelfContext.Domain.Entities.ShelvedBooks;
using ShelfContext.Domain.Entities.Shelves;

namespace ShelfContext.Tests.IntegrationTests.ReviewTests
{
    public class AddBookReviewRequestHandlerTests
    {
        private ShelfDbContext _db = null!;

        private AddBookReviewRequestHandler sut = null!;

        [SetUp]
        public void SetUp()
        {
            var options = new DbContextOptionsBuilder<ShelfDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;
            _db = new ShelfDbContext(options);

            sut = new AddBookReviewRequestHandler(
                new UnitOfWork(_db),
                new ShelvedBookRepository(_db),
                new ReviewRepository(_db));
        }

        [TearDown]
        public async Task TearDown()
        {
            await _db.DisposeAsync();
        }

        [Test]
        public async Task Creates_review()
        {
            var userId = new UserId(Guid.NewGuid());
            var bookId = new BookId(Guid.NewGuid());
            var shelf = Shelf.Create(userId, ShelfName.Create("name").Model).Model;
            var shelvedBook = ShelvedBook.Create(shelf.Id, bookId, userId);
            _db.AddRange(shelf, shelvedBook);
            await _db.SaveChangesAsync();

            var request = new AddBookReviewRequest()
            {
                BookId = bookId.Value,
                UserId = userId.Value,
                Text = "Review",
                Rating = 1,
            };
            var result = await sut.Handle(request, CancellationToken.None);

            Assert.That(result.IsSuccess, Is.True);
            Assert.That(await _db.Reviews.CountAsync(), Is.EqualTo(1));
            var review = await _db.Reviews.FirstAsync();
            Assert.Multiple(() =>
            {
                Assert.That(review.UserId, Is.EqualTo(userId));
                Assert.That(review.BookId, Is.EqualTo(bookId));
                Assert.That(review.Text.Value, Is.EqualTo("Review"));
                Assert.That(review.Rating.Value, Is.EqualTo(1));
            });
        }

        [Test]
        public async Task Fails_when_book_is_reviewed_by_the_same_user()
        {
            var userId = new UserId(Guid.NewGuid());
            var bookId = new BookId(Guid.NewGuid());
            var shelf = Shelf.Create(userId, ShelfName.Create("name").Model).Model;
            var shelvedBook = ShelvedBook.Create(shelf.Id, bookId, userId);
            _db.AddRange(shelf, shelvedBook);
            var existingReview = Review.Create(
                userId, bookId, Rating.Min, 
                ReviewText.Blank);
            _db.Reviews.Add(existingReview);
            await _db.SaveChangesAsync();

            var request = new AddBookReviewRequest()
            {
                BookId = bookId.Value,
                UserId = userId.Value,
                Text = "Review",
                Rating = 1,
            };
            var result = await sut.Handle(request, CancellationToken.None);

            Assert.That(result.IsFailure, Is.True);
        }

        [Test]
        public async Task Fails_when_does_not_have_book_shelved()
        {
            var userId = new UserId(Guid.NewGuid());
            var bookId = new BookId(Guid.NewGuid());

            var request = new AddBookReviewRequest()
            {
                BookId = bookId.Value,
                UserId = userId.Value,
                Text = "Review",
                Rating = 1,
            };
            var result = await sut.Handle(request, CancellationToken.None);

            Assert.That(result.IsFailure, Is.True);
        }
    }
}
