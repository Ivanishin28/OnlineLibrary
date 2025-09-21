using MediatR;
using Microsoft.AspNetCore.Mvc;
using ShelfContext.Contract.Commands.ShelveBook;
using ShelfContext.Contract.Queries.GetShelvedBookByBookId;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
        public async Task<IActionResult> Shelve([FromBody] ShelveBookRequest request)
        {
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
    }
}
