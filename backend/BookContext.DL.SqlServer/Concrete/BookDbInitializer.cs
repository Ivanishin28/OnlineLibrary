using BookContext.Domain.Entities;
using BookContext.Domain.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Shared.DL.Interfaces;


namespace BookContext.DL.SqlServer.Concrete
{
    public class BookDbInitializer : IDbInitializer
    {
        private BookDbContext _db;

        public BookDbInitializer(BookDbContext db)
        {
            _db = db;
        }

        public async Task Initialize()
        {
            await _db.Database.MigrateAsync();

            if (await _db.Genres.AnyAsync())
            {
                return;
            }

            var genres = new Genre[]
            {
                new Genre(new GenreId(new Guid("00000000-0000-0000-0000-000000000001")), "Fiction"),
                new Genre(new GenreId(new Guid("00000000-0000-0000-0000-000000000002")), "Non-Fiction"),
                new Genre(new GenreId(new Guid("00000000-0000-0000-0000-000000000003")), "Mystery & Thriller"),
                new Genre(new GenreId(new Guid("00000000-0000-0000-0000-000000000004")), "Science Fiction"),
                new Genre(new GenreId(new Guid("00000000-0000-0000-0000-000000000005")), "Fantasy"),
                new Genre(new GenreId(new Guid("00000000-0000-0000-0000-000000000006")), "Romance"),
                new Genre(new GenreId(new Guid("00000000-0000-0000-0000-000000000007")), "Historical Fiction"),
                new Genre(new GenreId(new Guid("00000000-0000-0000-0000-000000000008")), "Horror"),
                new Genre(new GenreId(new Guid("00000000-0000-0000-0000-000000000009")), "Biography & Memoir"),
                new Genre(new GenreId(new Guid("00000000-0000-0000-0000-00000000000A")), "Self-Help"),
                new Genre(new GenreId(new Guid("00000000-0000-0000-0000-00000000000B")), "Poetry"),
                new Genre(new GenreId(new Guid("00000000-0000-0000-0000-00000000000C")), "Young Adult (YA)"),
                new Genre(new GenreId(new Guid("00000000-0000-0000-0000-00000000000D")), "Children’s Books"),
                new Genre(new GenreId(new Guid("00000000-0000-0000-0000-00000000000E")), "Classics"),
                new Genre(new GenreId(new Guid("00000000-0000-0000-0000-00000000000F")), "Graphic Novels & Comics"),
                new Genre(new GenreId(new Guid("00000000-0000-0000-0000-000000000010")), "History"),
                new Genre(new GenreId(new Guid("00000000-0000-0000-0000-000000000011")), "Philosophy"),
                new Genre(new GenreId(new Guid("00000000-0000-0000-0000-000000000012")), "Religion & Spirituality"),
                new Genre(new GenreId(new Guid("00000000-0000-0000-0000-000000000013")), "Science & Technology"),
                new Genre(new GenreId(new Guid("00000000-0000-0000-0000-000000000014")), "Travel & Adventure"),
            };

            _db.Genres.AddRange(genres);
            await _db.SaveChangesAsync();
        }
    }
}
