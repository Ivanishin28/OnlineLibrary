using MediatR;
using Microsoft.AspNetCore.Mvc;
using Shared.Core.Models;
using ShelfContext.Contract.Commands;
using ShelfContext.Contract.Commands.CreateShelf;
using ShelfContext.Contract.Commands.EditShelf;
using ShelfContext.Contract.Errors;
using ShelfContext.Contract.Queries.GetShelvesByUserId;
using ShelfContext.Contract.Services;

namespace ShelfContext.Application.Controllers
{
    public class ShelfController : BaseShelfController
    {
        private IMediator _mediator;
        private IResouceAccessibilityChecker _checker;

        public ShelfController(IMediator mediator, IResouceAccessibilityChecker checker)
        {
            _mediator = mediator;
            _checker = checker;
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

        [HttpGet("user/{userId}")]
        public async Task<IActionResult> Get(Guid userId)
        {
            var query = new GetShelvesByUserIdRequest(userId);

            var result = await _mediator.Send(query);

            return Ok(result);
        }


        [HttpDelete("delete/{shelfId}")]
        public async Task<IActionResult> Delete(Guid shelfId)
        {
            if (!(await _checker.IsShelfAccesibleToUser(shelfId, GetUserId())))
            {
                return FromResult(Result.Failure(AccessibilityErrors.INACCESSIBLE));
            }

            var command = new DeleteShelfRequest(shelfId);
            var result = await _mediator.Send(command);
            return FromResult(result);
        }
    }
}
