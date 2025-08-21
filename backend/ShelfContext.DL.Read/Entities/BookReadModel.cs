using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShelfContext.DL.Read.Entities
{
    public class BookReadModel : ReadModel
    {
        public ICollection<ShelvedBookReadModel> ShelvedBooks { get; set; } = null!;
    }
}
