using Microsoft.AspNetCore.Mvc;
using Shared.Application.Controllers;

namespace ShelfContext.Application.Controllers
{
    [Route("api/shelf/[controller]")]
    public class BaseShelfController : BaseController
    {
    }
}
