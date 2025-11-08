using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShelfContext.Contract.Events
{
    public record BookDislodgedEvent : INotification
    {
        public required Guid UserId { get; init; }
        public required Guid BookId { get; init; }
        public required Guid ShelvedBookId { get; init; }
    }
}
