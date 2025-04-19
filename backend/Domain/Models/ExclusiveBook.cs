using Core.Models;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models
{
    public interface IExclusiveBookRepository
    {
        Task<ExclusiveBook> GetBy(Guid bookId, Guid userId);    
    }

    public class ExclusiveBookRepository : IExclusiveBookRepository
    {
        public async Task<ExclusiveBook> GetBy(Guid bookId, Guid userId)
        {
            var bookOnExclusiveShelf = new ShelvedBook();
            var exclusiveBook = new ExclusiveBook() { InitialBook = bookOnExclusiveShelf };
            return exclusiveBook;
        }

        public async Task Update(ExclusiveBook exclusiveBook)
        {
            var shelvedBooks = new List<ShelvedBook>();
            shelvedBooks.Remove(exclusiveBook.InitialBook);
            shelvedBooks.Add(exclusiveBook.NewBook);
        }
    }

    public class ExclusiveBook
    {
        public ShelvedBook InitialBook { get; set; }
        public ShelvedBook NewBook { get; set; }
        public void Reshelve(Book book, Shelf shelf)
        {
            NewBook = new ShelvedBook();
        }
    }
}
