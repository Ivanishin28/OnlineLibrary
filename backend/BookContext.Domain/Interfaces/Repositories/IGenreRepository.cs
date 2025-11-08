using BookContext.Domain.Entities;
using BookContext.Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookContext.Domain.Interfaces.Repositories
{
    public interface IGenreRepository
    {
        Task<ICollection<Genre>> GetAll();
        Task<ICollection<GenreId>> EnsureExist(ICollection<GenreId> ids);
    }
}
