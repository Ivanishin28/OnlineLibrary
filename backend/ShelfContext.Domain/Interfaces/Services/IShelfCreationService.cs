using Shared.Core.Models;
using ShelfContext.Domain.DTOs;
using ShelfContext.Domain.Entities.Shelves;
using ShelfContext.Domain.Entities.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShelfContext.Domain.Interfaces.Services
{
    public interface IShelfCreationService
    {
        Task<Result<Shelf>> Create(UserId userId, ShelfDto dto);
    }
}
