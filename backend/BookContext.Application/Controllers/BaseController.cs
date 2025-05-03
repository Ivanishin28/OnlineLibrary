using BookContext.Application.Models;
using Microsoft.AspNetCore.Mvc;
using Shared.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookContext.Application.Controllers
{
    [Route("api/book/[controller]")]
    public class BaseController : ControllerBase
    {
        protected IActionResult FromResult(Result result)
        {
            if(result.IsFailure)
            {
                return BadRequest();
            }

            return Ok();
        }

        protected IActionResult FromResult<T>(Result<T> result)
        {
            var apiResponse = new ApiResponse<T>(result.Model, result.Errors.ToArray());

            if(result.IsFailure)
            {
                return BadRequest(apiResponse);
            }

            return Ok(apiResponse);
        }
    }
}
