using MediatR;
using Microsoft.AspNetCore.Mvc;
using ShelfContext.Application.Dtos.Commands;
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
        public async Task<IActionResult> Create(CreateTagRequestDto request)
        {
            var command = new CreateTagRequest(GetUserId(), request.Name);
            var response = await _mediator.Send(command);

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
