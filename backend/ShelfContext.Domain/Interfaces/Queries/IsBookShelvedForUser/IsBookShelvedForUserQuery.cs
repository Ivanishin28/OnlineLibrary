using MediatR;
using ShelfContext.Domain.Entities.Books;
using ShelfContext.Domain.Entities.Shelves;
using ShelfContext.Domain.Entities.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShelfContext.Domain.Interfaces.Queries.IsBookShelvedForUser
{
    public class IsBookShelvedForUserQuery : IRequest<bool>
    {
        public BookId BookId { get; private set; }
        public UserId UserId { get; private set; }

        public IsBookShelvedForUserQuery(BookId bookId, UserId userId)
        {
            BookId = bookId;
            UserId = userId;
        }
    }
}
