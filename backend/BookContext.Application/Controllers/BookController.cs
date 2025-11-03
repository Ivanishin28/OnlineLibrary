using BookContext.Application.Dtos.Commands;
using BookContext.Contract.Commands;
using BookContext.Contract.Commands.CreateBook;
using BookContext.Contract.Queries;
using BookContext.Contract.Queries.GetAllBooks;
using BookContext.Contract.Queries.GetFullBook;
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

        [HttpGet("search")]
        public async Task<IActionResult> Search([FromQuery] string query)
        {
            var searchQuery = new SearchBookQuery(query);
            var result = await _metiator.Send(searchQuery);

            return Ok(result);
        }

        [HttpPost("create")]
        public async Task<IActionResult> Create(CreateBookRequestDto dto)
        {
            var request = new CreateBookRequest()
            {
                CreatorId = GetUserId(),
                Title = dto.Title,
                AuthorIds = dto.AuthorIds,
                PublishingDate = dto.PublishingDate,
                CoverId = dto.CoverId,
                Description = dto.Description
            };
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

        [HttpDelete("delete/{bookId}")]
        public async Task<IActionResult> Delete(Guid bookId)
        {
            var request = new DeleteBookRequest(bookId);
            var result = await _metiator.Send(request);

            return FromResult(result);
        }

        [HttpPost("update")]
        public async Task<IActionResult> Update(UpdateBookRequest request)
        {
            var result = await _metiator.Send(request);
            return FromResult(result);
        }
    }
}
