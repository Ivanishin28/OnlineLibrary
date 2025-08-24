using Microsoft.AspNetCore.Mvc;
using Shared.Application.Models;
using Shared.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.Application.Controllers
{
    [ApiController]
    public abstract class BaseController : ControllerBase
    {
        protected IActionResult FromResult(Result result)
        {
            var apiResponse = new ApiResponse(result.Errors.ToArray());

            if(result.IsFailure)
            {
                return BadRequest(apiResponse);
            }

            return Ok();
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
    }
}
