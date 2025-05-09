using Microsoft.AspNetCore.Mvc;
using Shared.Application.Controllers;

namespace BookContext.Application.Controllers
{
    [Route("api/book/[controller]")]
    public abstract class BaseBookController : BaseController
    {
    }
}
