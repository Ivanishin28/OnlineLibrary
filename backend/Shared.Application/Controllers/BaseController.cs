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
    [Route("api/book/[controller]")]
    [ApiController]
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
