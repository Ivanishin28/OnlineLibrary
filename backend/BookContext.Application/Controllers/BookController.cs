using BookContext.Contract.Commands.CreateBook;
using BookContext.Contract.Commands.UpdateBook;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json.Serialization;

namespace BookContext.Application.Controllers
{
    public class BookController : BaseController
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
    }
}
