using MediatR;
using Microsoft.AspNetCore.Mvc;
using Shared.Application.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserContext.Contract.Commands.CreateUser;
using UserContext.Contract.Queries;

namespace UserContext.Application.Controllers
{
    public class UserController : BaseUserController
    {
        private IMediator _mediator;

        public UserController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("create")]
        public async Task<IActionResult> Create([FromBody] CreateUserRequest request)
        {
            var result = await _mediator.Send(request);
            return FromResult(result);
        }

        [HttpGet("preview/{userId}")]
        public async Task<IActionResult> GetUserByUserId(Guid userId)
        {
            var query = new GetUserByUserIdQuery(userId);
            var result = await _mediator.Send(query);
            return Ok(result);
        }
    }
}
