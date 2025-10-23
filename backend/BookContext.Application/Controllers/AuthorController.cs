using BookContext.Contract.Commands.CreateAuthor;
using BookContext.Contract.Dtos.Commands;
using BookContext.Contract.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace BookContext.Application.Controllers
{
    public class AuthorController : BaseBookController
    {
        private IMediator _mediator;

        public AuthorController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("userId/{userId}")]
        public async Task<IActionResult> GetByUserId(Guid userId)
        {
            var request = new GetUserAuthorsQuery(userId);
            var result = await _mediator.Send(request);
            return Ok(result);
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
