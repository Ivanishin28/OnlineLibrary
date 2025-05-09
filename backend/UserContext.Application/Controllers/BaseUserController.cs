using Microsoft.AspNetCore.Mvc;
using Shared.Application.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UserContext.Application.Controllers
{
    [Route("api/user/[controller]")]
    public abstract class BaseUserController : BaseController
    {
    }
}
