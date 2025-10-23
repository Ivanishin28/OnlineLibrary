﻿using BookContext.Domain.Errors;
using BookContext.Domain.ValueObjects;
using Shared.Core.Models;

namespace BookContext.Domain.Entities
{
    public class Book
    {
        private List<BookAuthor> _bookAuthors = new List<BookAuthor>();

        public BookId Id { get; private set; } = null!;
        public UserId CreatorId { get; private set; } = null!;
        public string Title { get; private set; } = null!;
        public IReadOnlyCollection<BookAuthor> BookTags => _bookAuthors.AsReadOnly();

        private Book() { }

        private Book(BookId id, string title, UserId creatorId)
        {
            Id = id;
            Title = title;
            CreatorId = creatorId;
        }

        public static Result<Book> Create(UserId creatorId, string title)
        {
            return Create(creatorId, title, new List<AuthorId>());
        }

        public static Result<Book> Create(UserId creatorId, string title, ICollection<AuthorId> authorIds)
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

            return new Book(bookId, title, creatorId);
        }

        public Result AddAuthor(AuthorId authorId, DateTime creationTime)
        {
            if (_bookAuthors.Any(x => x.Id == authorId))
            {
                return Result.Failure(BookErrors.DuplicateAuthors); 
            }

            var baId = new BookAuthorId(Guid.NewGuid());
            var bookAuthor = new BookAuthor(baId, Id, authorId, creationTime);
            _bookAuthors.Add(bookAuthor);

            return Result.Success();
        }

        public Result RemoveAuthor(AuthorId authorId)
        {
            var bookAuthor = _bookAuthors.FirstOrDefault(x => x.Id == authorId);
            if (bookAuthor is null)
            {
                return Result.Failure(BookErrors.AuthorNotFound);
            }

            _bookAuthors.Remove(bookAuthor);
            return Result.Success();
        }
    }
}
