using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShelfContext.Application.Models
{
    public record Resouces
    {
        public Guid? TagId { get; init; }
        public Guid? ShelfId { get; init; }
        public Guid? ShelvedBookId { get; init; }
        public Guid? BookId { get; init; }
    }
}
