using ShelfContext.Contract.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShelfContext.Tests.Fakes
{
    internal class AllAccessibleResouceAccessibilityChecker : IResouceAccessibilityChecker
    {
        public Task<bool> IsBookAccessibleToUser(Guid bookId, Guid userId)
        {
            return Task.FromResult(true);
        }

        public Task<bool> IsShelfAccesibleToUser(Guid shelfId, Guid userId)
        {
            return Task.FromResult(true);
        }

        public Task<bool> IsTagAccessibleToUser(Guid tagId, Guid userId)
        {
            return Task.FromResult(true);
        }
    }
}
