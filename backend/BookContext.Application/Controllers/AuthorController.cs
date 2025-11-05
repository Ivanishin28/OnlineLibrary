using BookContext.Application.Dtos.Commands;
using BookContext.Contract.Commands;
using BookContext.Contract.Commands.CreateAuthor;
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

        [HttpGet("search")]
        public async Task<IActionResult> Search([FromQuery] string query)
        {
            var searchQuery = new SearchAuthorQuery(query);
            var result = await _mediator.Send(searchQuery);
            return Ok(result);
        }

        [HttpGet("full/{id}")]
        public async Task<IActionResult> GetFull(Guid id)
        {
            var query = new GetFullAuthorQuery(id);
            var result = await _mediator.Send(query);
            return Ok(result);
        }

        [HttpPost("create")]
        public async Task<IActionResult> Create(CreateAuthorRequestDto dto)
        {
            var request = new CreateAuthorRequest()
            {
                CreatorId = GetUserId(),
                FirstName = dto.FirstName,
                LastName = dto.LastName,
                BirthDate = dto.BirthDate,
                AvatarId = dto.AvatarId,
                Biography = dto.Biography
            };
            var createAuthorResult = await _mediator.Send(request);
            return FromResult(createAuthorResult);
        }

        [HttpDelete("delete/{authorId}")]
        public async Task<IActionResult> Delete(Guid authorId)
        {
            var request = new DeleteAuthorRequest(authorId);
            var result = await _mediator.Send(request);
            return FromResult(result);
        }

        [HttpPost("update")]
        public async Task<IActionResult> Update(UpdateAuthorRequestDto dto)
        {
            var request = new UpdateAuthorRequest()
            {
                Id = dto.Id,
                Biography = dto.Biography,
                BirthDate = dto.BirthDate,
                AvatarId = dto.AvatarId
            };
            var result = await _mediator.Send(request);
            return FromResult(result);
        }

        [HttpGet("full-name-taken")]
        public async Task<IActionResult> IsFullNameTaken([FromQuery] string firstName, [FromQuery] string lastName, [FromQuery] string? middleName = null)
        {
            var query = new IsAuthorFullNameTakenQuery(firstName, lastName, middleName);
            var result = await _mediator.Send(query);
            return Ok(result);
        }
    }
}
