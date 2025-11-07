using BookContext.Domain.Errors;
using BookContext.Domain.ValueObjects;
using Shared.Core.Models;

namespace BookContext.Domain.Entities
{
    public class Book
    {
        private List<BookAuthor> _bookAuthors = new List<BookAuthor>();
        private List<BookGenre> _bookGenres = new List<BookGenre>();

        public BookId Id { get; private set; } = null!;
        public UserId CreatorId { get; private set; } = null!;
        public string Title { get; private set; } = null!;
        public IReadOnlyCollection<BookAuthor> BookAuthors => _bookAuthors.AsReadOnly();
        public IReadOnlyCollection<BookGenre> BookGenres => _bookGenres.AsReadOnly();

        private Book() { }

        private Book(BookId id, string title, UserId creatorId, List<BookAuthor> authors, List<BookGenre> genres)
        {
            Id = id;
            Title = title;
            CreatorId = creatorId;
            _bookAuthors = authors;
            _bookGenres = genres;
        }

        public static Result<Book> Create(UserId creatorId, string title)
        {
            return Create(creatorId, title, new List<AuthorId>(), new List<GenreId>());
        }

        public static Result<Book> Create(
            UserId creatorId, 
            string title, 
            ICollection<AuthorId> authorIds, 
            ICollection<GenreId> genreIds)
        {
            var bookId = new BookId(Guid.NewGuid());

            if (String.IsNullOrEmpty(title))
            {
                return Result<Book>.Failure(BookErrors.EmptyTitle);
            }
            if (authorIds.Select(x => x.Value).Distinct().Count() != authorIds.Count)
            {
                return Result<Book>.Failure(BookErrors.DuplicateAuthors);
            }
            if (genreIds.Select(x => x.Value).Distinct().Count() != genreIds.Count)
            {
                return Result<Book>.Failure(BookErrors.DuplicateGenres);
            }

            var bookAuthors = authorIds
                .Select(x => new BookAuthor(
                    new BookAuthorId(Guid.NewGuid()),
                    bookId, x, DateTime.Now))
                .ToList();

            var bookGenres = genreIds
                .Select(x => new BookGenre(
                    new BookGenreId(Guid.NewGuid()),
                    bookId, x, DateTime.Now))
                .ToList();

            return new Book(bookId, title, creatorId, bookAuthors, bookGenres);
        }

        public Result AddAuthor(AuthorId authorId)
        {
            if (_bookAuthors.Any(x => x.AuthorId == authorId))
            {
                return Result.Failure(BookErrors.DuplicateAuthors); 
            }

            var baId = new BookAuthorId(Guid.NewGuid());
            var bookAuthor = new BookAuthor(baId, Id, authorId, DateTime.Now);
            _bookAuthors.Add(bookAuthor);

            return Result.Success();
        }

        public Result RemoveAuthor(AuthorId authorId)
        {
            var bookAuthor = _bookAuthors.FirstOrDefault(x => x.AuthorId == authorId);
            if (bookAuthor is null)
            {
                return Result.Failure(BookErrors.AuthorNotFound);
            }

            _bookAuthors.Remove(bookAuthor);
            return Result.Success();
        }

        public bool HasAuthor(AuthorId authorId)
        {
            return _bookAuthors.Any(x => x.AuthorId == authorId);
        }

        public Result AddGenre(GenreId genreId)
        {
            if (_bookGenres.Any(x => x.GenreId == genreId))
            {
                return Result.Failure(BookErrors.DuplicateGenres);
            }

            var bgId = new BookGenreId(Guid.NewGuid());
            var bookGenre = new BookGenre(bgId, Id, genreId, DateTime.Now);
            _bookGenres.Add(bookGenre);

            return Result.Success();
        }

        public Result RemoveGenre(GenreId genreId)
        {
            var bookGenre = _bookGenres.FirstOrDefault(x => x.GenreId == genreId);
            if (bookGenre is null)
            {
                return Result.Failure(BookErrors.GenreNotFound);
            }

            _bookGenres.Remove(bookGenre);
            return Result.Success();
        }
    }
}
