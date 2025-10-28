using Composition.Contract.Queries;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Shared.Application.Controllers;

namespace Composition.Application.Controllers
{
    [Authorize]
    [Route("api/composition/[controller]")]
    public class ReviewController : BaseController
    {
        private IMediator _mediator;

        public ReviewController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("reviews")]
        public async Task<IActionResult> GetLibraryPage(GetBookReviewsWithIdentitiesQuery request)
        {
            var result = await _mediator.Send(request);
            return Ok(result);
        }
    }
}
