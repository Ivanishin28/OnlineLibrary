using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShelfContext.DL.Read.Entities
{
    public class UserReadModel : ReadModel
    {
        public ICollection<ShelfReadModel> Shelves { get; set; }
        public ICollection<TagReadModel> Tags { get; set; }
    }
}
