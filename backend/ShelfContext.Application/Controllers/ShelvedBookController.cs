using MediatR;
using Microsoft.AspNetCore.Mvc;
using Shared.Core.Models;
using ShelfContext.Application.Dtos.Commands;
using ShelfContext.Contract.Commands.AddTagToBook;
using ShelfContext.Contract.Commands.RemoveTag;
using ShelfContext.Contract.Commands.ShelveBook;
using ShelfContext.Contract.Errors;
using ShelfContext.Contract.Queries.GetShelvedBookByBookId;
using ShelfContext.Contract.Services;

namespace ShelfContext.Application.Controllers
{
    public class ShelvedBookController : BaseShelfController
    {
        private IMediator _mediator;
        private IResouceAccessibilityChecker _checker;

        public ShelvedBookController(IMediator mediator, IResouceAccessibilityChecker checker)
        {
            _mediator = mediator;
            _checker = checker;
        }

        [HttpPost("shelve")]
        public async Task<IActionResult> Shelve(ShelveBookDto dto)
        {
            var request = new ShelveBookRequest(
                dto.BookId, 
                dto.ShelfId, 
                GetUserId());
            var response = await _mediator.Send(request);

            return FromResult(response);
        }

        [HttpGet("user/{userId}/book/{bookId}")]
        public async Task<IActionResult> GetBy(Guid userId, Guid bookId)
        {
            var request = new GetShelvedBookByBookIdRequest(userId, bookId);
            var result = await _mediator.Send(request);
            return Ok(result);
        }

        [HttpPost("add-tag")]
        public async Task<IActionResult> AddTag(AddTagToBookDto request)
        {
            var command = new AddTagToBookRequest(request.ShelvedBookId, request.TagId, GetUserId());
            var result = await _mediator.Send(command);
            return FromResult(result);
        }

        [HttpPost("remove-tag")]
        public async Task<IActionResult> RemoveTag(RemoveTagFromBookDto request)
        {
            var userId = GetUserId();
            if (!(await _checker.IsTagAccessibleToUser(request.TagId, userId)) ||
                !(await _checker.IsShelvedBookAccessibleToUser(request.ShelvedBookId, userId)))
            {
                return FromResult(Result.Failure(AccessibilityErrors.INACCESSIBLE));
            }

            var command = new RemoveTagRequest(request.ShelvedBookId, request.TagId, GetUserId());
            var result = await _mediator.Send(command);
            return FromResult(result);
        }
    }
}
