using MediatR;
using Microsoft.AspNetCore.Mvc;
using ShelfContext.Contract.Commands.CreateTag;

namespace ShelfContext.Application.Controllers
{
    public class TagController : BaseShelfController
    {
        private IMediator _mediator;

        public TagController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("create")]
        public async Task<IActionResult> Create([FromBody] CreateTagRequest request)
        {
            var response = await _mediator.Send(request);

            return FromResult(response);
        }
    }
}
