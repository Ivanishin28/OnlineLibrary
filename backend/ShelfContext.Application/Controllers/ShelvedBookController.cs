using MediatR;
using Microsoft.AspNetCore.Mvc;
using ShelfContext.Contract.Commands.ShelveBook;
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
    }
}
