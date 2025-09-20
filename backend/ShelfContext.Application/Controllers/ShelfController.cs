using MediatR;
using Microsoft.AspNetCore.Mvc;
using ShelfContext.Contract.Commands.CreateShelf;
using ShelfContext.Contract.Commands.EditShelf;
using ShelfContext.Contract.Queries.GetShelvesByUserId;

namespace ShelfContext.Application.Controllers
{
    public class ShelfController : BaseShelfController
    {
        private IMediator _mediator;

        public ShelfController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("create")]
        public async Task<IActionResult> Create([FromBody] CreateShelfRequest request)
        {
            var response = await _mediator.Send(request);

            return FromResult(response);
        }

        [HttpPost("edit")]
        public async Task<IActionResult> Edit([FromBody] EditShelfRequest request)
        {
            var response = await _mediator.Send(request);

            return FromResult(response);
        }

        [HttpGet("user/{userId}")]
        public async Task<IActionResult> Get(Guid userId)
        {
            var query = new GetShelvesByUserIdRequest(userId);

            var result = await _mediator.Send(query);

            return Ok(result);
        }
    }
}
