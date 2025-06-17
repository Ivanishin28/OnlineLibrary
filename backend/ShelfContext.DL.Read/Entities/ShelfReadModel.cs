using ShelfContext.Domain.Entities.ShelvedBooks;
using ShelfContext.Domain.Entities.Shelves;
using ShelfContext.Domain.Entities.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShelfContext.DL.Read.Entities
{
    public class ShelfReadModel : ReadModel
    {
        public Guid UserId { get; private set; }
        public string Name { get; private set; }
        public DateTime DateCreated { get; private set; }

        public UserReadModel User { get; set; }
        public ICollection<ShelvedBookReadModel> ShelvedBooks { get; set; }
    }
}
