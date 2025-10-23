using BookContext.Contract.Commands.CreateAuthor;
using BookContext.Contract.Dtos.Commands;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Shared.Application.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookContext.Application.Controllers
{
    public class AuthorController : BaseBookController
    {
        private IMediator _mediator;

        public AuthorController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("create")]
        public async Task<IActionResult> Create(CreateAuthorRequestDto dto)
        {
            var request = new CreateAuthorRequest(
                GetUserId(), 
                dto.FirstName, 
                dto.LastName, 
                dto.BirthDate);
            var createAuthorResult = await _mediator.Send(request);
            return FromResult(createAuthorResult);
        }
    }
}
