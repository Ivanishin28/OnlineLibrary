using Shared.Core.Models;
using ShelfContext.Application.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShelfContext.Application.Interfaces
{
    public interface IResouceAuthChecker
    {
        Task<Result> CheckResouceAccessibilityToUser(Guid userId, Resouces resouces);
    }
}
