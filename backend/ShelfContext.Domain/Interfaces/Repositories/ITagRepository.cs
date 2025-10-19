using ShelfContext.Domain.Entities.Tags;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShelfContext.Domain.Interfaces.Repositories
{
    public interface ITagRepository
    {
        Task<Tag?> GetBy(TagId id);
        
        void Add(Tag tag);
        void Remove(Tag tag);
    }
}
