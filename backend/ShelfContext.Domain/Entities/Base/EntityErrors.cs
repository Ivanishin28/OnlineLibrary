using Shared.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShelfContext.Domain.Entities.Base
{
    public static class EntityErrors
    {
        public static Error NotFound = new Error("Entity.NotFound", "Entity.NotFound");
    }
}
