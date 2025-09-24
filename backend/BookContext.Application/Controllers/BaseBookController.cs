using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Shared.Application.Controllers;

namespace BookContext.Application.Controllers
{
    [Authorize]
    [Route("api/book/[controller]")]
    public abstract class BaseBookController : BaseController
    {
    }
}
