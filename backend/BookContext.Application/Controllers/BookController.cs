using BookContext.Contract.Commands.CreateBook;
using BookContext.Contract.Queries;
using BookContext.Contract.Queries.GetAllBooks;
using BookContext.Contract.Queries.GetBook;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace BookContext.Application.Controllers
{
    public class BookController : BaseBookController
    {
        private IMediator _metiator;

        public BookController(IMediator metiator)
        {
            _metiator = metiator;
        }

        [HttpGet("all")]
        public async Task<IActionResult> All()
        {
            var request = new GetAllBooksQuery();

            var result = await _metiator.Send(request);

            return Success(result);
        }

        [HttpPost("create")]
        public async Task<IActionResult> Create([FromBody] CreateBookRequest request)
        {
            var result = await _metiator.Send(request);

            return FromResult(result);
        }

        [HttpGet("userId/{userId}")]
        public async Task<IActionResult> GetUserBooks(Guid userId)
        {
            var request = new GetUserBooksQuery(userId);
            var result = await _metiator.Send(request);

            return Ok(result);
        }

        [HttpGet("full/{id}")]
        public async Task<IActionResult> GetFull(Guid id)
        {
            var query = new GetFullBookQuery(id);

            var book = await _metiator.Send(query);

            return Ok(book);
        }
    }
}
