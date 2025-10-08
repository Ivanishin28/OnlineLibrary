using MediatR;
using Microsoft.AspNetCore.Mvc;
using ShelfContext.Contract.Commands.CreateTag;
using ShelfContext.Contract.Queries;

namespace ShelfContext.Application.Controllers
{
    public class TagController : BaseShelfController
    {
        private IMediator _mediator;

        public TagController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("user/{userId}")]
        public async Task<IActionResult> GetAllUserTags(Guid userId)
        {
            var query = new GetUserTagsQuery(userId);
            var result = await _mediator.Send(query);
            return Ok(result);
        }

        [HttpPost("create")]
        public async Task<IActionResult> Create([FromBody] CreateTagRequest request)
        {
            var response = await _mediator.Send(request);

            return FromResult(response);
        }

        [HttpGet("name-taken")]
        public async Task<IActionResult> IsNameTaken([FromQuery] string name, [FromQuery] Guid? userId)
        {
            var query = new IsTagNameTakenQuery(userId ?? GetUserId(), name);
            var result = await _mediator.Send(query);

            return Ok(result);
        }
    }
}
