using IdentityContext.Contracts.Commands;
using IdentityContext.Contracts.Commands.Login;
using IdentityContext.Contracts.Commands.Register;
using IdentityContext.Contracts.Dtos;
using IdentityContext.Contracts.Queries;
using MediatR;
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

        [HttpGet("preview/{userId}")]
        public async Task<IActionResult> GetIdentityPreviewByUserId(Guid userId)
        {
            var query = new GetIdentityPreviewByUserIdQuery(userId);
            var result = await _mediator.Send(query);
            return Ok(result);
        }

        [HttpPost("set-avatar")]
        public async Task<IActionResult> SetAvatar(SetAvatarRequestDto dto)
        {
            var request = new SetAvatarRequest(GetUserId(), dto.AvatarId);
            await _mediator.Send(request);
            return Ok();
        }
    }
}
