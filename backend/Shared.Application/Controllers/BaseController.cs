using IdentityContext.Application.Consts;
using Microsoft.AspNetCore.Mvc;
using Shared.Application.Models;
using Shared.Core.Models;

namespace Shared.Application.Controllers
{
    [ApiController]
    public abstract class BaseController : ControllerBase
    {
        protected IActionResult FromResult(Result result)
        {
            var apiResponse = new ApiResponse(result.Errors.ToArray());

            return result.IsSuccess ? Ok(apiResponse) : BadRequest(apiResponse);
        }

        protected IActionResult FromResult<T>(Result<T> result)
        {
            if(result.IsFailure)
            {
                var apiResponse = new ApiResponse<T>(
                    default,
                    result.Errors.ToArray());

                return BadRequest(apiResponse);
            }
            else
            {
                var apiResponse = new ApiResponse<T>(
                    result.Model, 
                    Array.Empty<Error>());

                return Ok(apiResponse);
            }
        }

        protected IActionResult Success<T>(T model)
        {
            var apiResponse = new ApiResponse<T>(model, Array.Empty<Error>());

            return Ok(apiResponse);
        }

        protected Guid GetUserId()
        {
            var user = User
                .Claims
                .First(x => x.Type == Claims.USER_ID);

            return new Guid(user.Value);
        }
    }
}
