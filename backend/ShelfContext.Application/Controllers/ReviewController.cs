using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ShelfContext.Application.Dtos.Commands;
using ShelfContext.Contract.Commands;
using ShelfContext.Contract.Queries;

namespace ShelfContext.Application.Controllers
{
    public class ReviewController : BaseShelfController
    {
        private readonly IMediator _mediator;

        public ReviewController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<IActionResult> AddReview(AddBookReviewRequestDto dto)
        {
            var request = new AddBookReviewRequest
            {
                UserId = GetUserId(),
                BookId = dto.BookId,
                Rating = dto.Rating,
                Text = dto.Text
            };

            var result = await _mediator.Send(request);

            return FromResult(result);
        }

        [HttpGet("statistics/{bookId}")]
        public async Task<IActionResult> GetStatistics(Guid bookId)
        {
            var query = new GetBookReviewStatisticsQuery(bookId);
            var result = await _mediator.Send(query);
            return Ok(result);
        }
    }
}

