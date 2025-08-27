using Microsoft.AspNetCore.Mvc;
using Shared.Application.Controllers;

namespace IdentityContext.Application.Controllers
{
    [Route("api/identity/[controller]")]
    public class ApplicationUserController : BaseController
    {
        public async Task<IActionResult> Register()
        {
            return Ok("");
        }
    }
}
