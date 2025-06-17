using Microsoft.EntityFrameworkCore;
using ShelfContext.DL.Read;
using ShelfContext.DL.Read.Entities;

namespace ShelfContext.Tests.Infrastructure.Queries
{
    public class ReadDbContextTests
    {

        private ShelfReadDbContext _db;

        [SetUp]
        public void SetUp()
        {
            var options = new DbContextOptionsBuilder<ShelfReadDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;

            _db = new ShelfReadDbContext(options);
        }

        [TearDown]
        public void TearDown()
        {
            _db.Dispose();
        }

        [Test]
        public async Task DbSet_Count_Empty()
        {
            Assert.That(_db.Books.Count() == 0);
            Assert.That(_db.ShelvedBooks.Count() == 0);
            Assert.That(_db.Shelves.Count() == 0);
            Assert.That(_db.Tags.Count() == 0);
            Assert.That(_db.Users.Count() == 0);
            Assert.That(_db.BookTags.Count() == 0);
        }

        [Test]
        public void SaveChanges_AddedBook_SavesBook()
        {
            var book = new BookReadModel();

            _db.Books.Add(book);
            _db.SaveChanges();

            var savedBooks = _db.Books.ToList();

            Assert.That(savedBooks.Count() > 0);
        }

        [Test]
        public void SaveChanges_AddedUser_SavesUser()
        {
            var user = new UserReadModel();
            user.Shelves = new List<ShelfReadModel>()
            {
                new ShelfReadModel()
                {
                    Name = "Newshelf",
                    DateCreated = DateTime.UtcNow
                }
            };

            _db.Users.Add(user);
            _db.SaveChanges();

            Assert.That(_db.Users.Count() == 1);
            Assert.That(_db.Shelves.Count() == 1);
        }
    }
}
