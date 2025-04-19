using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DL.Interfaces
{
    public interface IBookRepository
    {
        public Task Add(Book book);
    }
}
