using MediatR;
using Microsoft.AspNetCore.Mvc;
using Shared.Core.Models;
using ShelfContext.Application.Dtos.Commands;
using ShelfContext.Contract.Commands;
using ShelfContext.Contract.Commands.CreateTag;
using ShelfContext.Contract.Errors;
using ShelfContext.Contract.Queries;
using ShelfContext.Contract.Services;

namespace ShelfContext.Application.Controllers
{
    public class TagController : BaseShelfController
    {
        private IMediator _mediator;
        private IResouceAccessibilityChecker _checker;

        public TagController(IMediator mediator, IResouceAccessibilityChecker checker)
        {
            _mediator = mediator;
            _checker = checker;
        }

        [HttpGet("user/{userId}")]
        public async Task<IActionResult> GetAllUserTags(Guid userId)
        {
            var query = new GetUserTagsQuery(userId);
            var result = await _mediator.Send(query);
            return Ok(result);
        }

        [HttpPost("create")]
        public async Task<IActionResult> Create(CreateTagRequestDto request)
        {
            var command = new CreateTagRequest(GetUserId(), request.Name);
            var response = await _mediator.Send(command);

            return FromResult(response);
        }

        [HttpGet("name-taken")]
        public async Task<IActionResult> IsNameTaken([FromQuery] string name, [FromQuery] Guid? userId)
        {
            var query = new IsTagNameTakenQuery(userId ?? GetUserId(), name);
            var result = await _mediator.Send(query);

            return Ok(result);
        }

        [HttpPost("rename/{tagId}")]
        public async Task<IActionResult> Rename(Guid tagId, [FromQuery] string name)
        {
            if (!(await _checker.IsTagAccessibleToUser(tagId, GetUserId())))
            {
                return FromResult(Result.Failure(AccessibilityErrors.INACCESSIBLE));
            }

            var request = new RenameTagRequest()
            {
                TagId = tagId,
                Name = name
            };
            var result = await _mediator.Send(request);

            return FromResult(result);
        }

        [HttpDelete("delete/{tagId}")]
        public async Task<IActionResult> Delete(Guid tagId)
        {
            if (!(await _checker.IsTagAccessibleToUser(tagId, GetUserId())))
            {
                return FromResult(Result.Failure(AccessibilityErrors.INACCESSIBLE));
            }

            var command = new DeleteTagRequest(tagId);
            var result = await _mediator.Send(command);
            return FromResult(result);
        }
    }
}
