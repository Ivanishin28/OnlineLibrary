using IdentityContext.Contracts.Commands.Login;
using IdentityContext.Contracts.Commands.Register;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Shared.Application.Controllers;

namespace IdentityContext.Application.Controllers
{
    [Route("api/identity/[controller]")]
    public class ApplicationUserController : BaseController
    {
        private IMediator _mediator;

        public ApplicationUserController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("Register")]
        public async Task<IActionResult> Register(RegisterRequest request)
        {
            var result = await _mediator.Send(request);

            return FromResult(result);
        }

        [HttpPost("Login")]
        public async Task<IActionResult> Login(LoginRequest request)
        {
            var result = await _mediator.Send(request);

            return FromResult(result);
        }

        [Authorize]
        public async Task<IActionResult> Test()
        {
            return Ok(new int[] { 1, 2, 3 });
        }
    }
}
