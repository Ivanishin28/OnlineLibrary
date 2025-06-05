using MediatR;
using Microsoft.AspNetCore.Mvc;
using ShelfContext.Contract.Commands.CreateShelf;
using ShelfContext.Contract.Commands.EditShelf;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
    }
}
