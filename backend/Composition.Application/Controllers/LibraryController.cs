using Composition.Contract.Queries;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Shared.Application.Controllers;

namespace Composition.Application.Controllers
{
    [Authorize]
    [Route("api/composition/[controller]")]
    public class LibraryController : BaseController
    {
        private IMediator _mediator;

        public LibraryController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("page")]
        public async Task<IActionResult> GetLibraryPage(GetLibraryPageQuery query)
        {
            var result = await _mediator.Send(query);
            return Ok(result);
        }
    }
}
