using BookContext.Contract.Commands.CreateAuthor;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookContext.Application.Controllers
{
    public class AuthorController : BaseController
    {
        private IMediator _mediator;

        public AuthorController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("create_author")]
        public async Task<IActionResult> CreateAuthor([FromBody] CreateAuthorRequest request)
        {
            var createAuthorResult = await _mediator.Send(request);
            return FromResult(createAuthorResult);
        }
    }
}
