using IdentityContext.Application.Consts;
using Microsoft.AspNetCore.Http;
using Shared.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IdentityContext.Application.Concrete
{
    public class HttpUserContext : IUserContext
    {
        private IHttpContextAccessor _context;

        public HttpUserContext(IHttpContextAccessor context)
        {
            _context = context;
        }

        public Guid UserId
        {
            get
            {
                var user = _context
                    .HttpContext!
                    .User
                    .Claims.First(x => x.Type == Claims.USER_ID);

                return new Guid(user.Value);
            }
        }
    }
}
