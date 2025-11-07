using BookContext.Contract.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace BookContext.Application.Controllers
{
    public class GenreController : BaseBookController
    {
        private IMediator _mediator;

        public GenreController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("all")]
        public async Task<IActionResult> All()
        {
            var query = new GetAllGenresQuery();
            var result = await _mediator.Send(query);
            return Ok(result);
        }
    }
}

