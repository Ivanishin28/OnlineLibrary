using MediatR;
using Microsoft.AspNetCore.Mvc;
using ShelfContext.Application.Dtos.Commands;
using ShelfContext.Contract.Commands.AddTagToBook;
using ShelfContext.Contract.Commands.ShelveBook;
using ShelfContext.Contract.Queries.GetShelvedBookByBookId;

namespace ShelfContext.Application.Controllers
{
    public class ShelvedBookController : BaseShelfController
    {
        private IMediator _mediator;

        public ShelvedBookController(IMediator mediator)
        {
            _mediator = mediator;
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
    }
}
