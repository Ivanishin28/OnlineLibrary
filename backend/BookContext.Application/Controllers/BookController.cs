using BookContext.Contract.Commands.CreateBook;
using BookContext.Contract.Commands.UpdateBook;
using BookContext.Contract.Queries.GetBook;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Shared.Application.Controllers;
using Shared.Core.Models;
using System.Text.Json.Serialization;

namespace BookContext.Application.Controllers
{
    public class BookController : BaseBookController
    {
        private IMediator _metiator;

        public BookController(IMediator metiator)
        {
            _metiator = metiator;
        }

        [HttpPost("create")]
        public async Task<IActionResult> Create([FromBody] CreateBookRequest request)
        {
            var result = await _metiator.Send(request);

            return FromResult(result);
        }

        [HttpPut("update")]
        public async Task<IActionResult> Update([FromBody] UpdateBookRequest request)
        {
            var result = await _metiator.Send(request);

            return FromResult(result);
        }

        [HttpPost("full/{id}")]
        public async Task<IActionResult> GetFull(Guid id)
        {
            var query = new GetFullBookQuery(id);

            var book = await _metiator.Send(query);

            return Success(book);
        }
    }
}
