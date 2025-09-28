using Shared.Core.Models;
using ShelfContext.Domain.Entities.Books;
using ShelfContext.Domain.Entities.ShelvedBooks;
using ShelfContext.Domain.Entities.Shelves;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShelfContext.Domain.Interfaces.Services
{
    public interface IShelvingService
    {
        Result<ShelvedBook> Shelve(Shelf shelf, Book book);
        Result Reshelve(ShelvedBook book, Shelf shelf);
    }
}
