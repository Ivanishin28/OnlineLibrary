using MediaContext.Application.Contracts.Commands;
using MediaContext.Application.Contracts.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace MediaContext.Application.Controllers
{
    [ApiController]
    [Route("api/media/[controller]")]
    public class MediaFileController : ControllerBase
    {
        private IMediator _mediator;

        public MediaFileController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("upload")]
        public async Task<IActionResult> UploadFile([FromBody] UploadFileRequest request)
        {
            var result = await _mediator.Send(request);
            return result is not null ? Ok(result) : BadRequest();
        }

        [HttpGet("{fileId}")]
        public async Task<IActionResult> Download(Guid fileId)
        {
            var query = new GetFileQuery(fileId);
            var file = await _mediator.Send(query);
            if (file is null)
            {
                return BadRequest();
            }

            return File(file.Content, file.ContentType);
        }
    }
}
