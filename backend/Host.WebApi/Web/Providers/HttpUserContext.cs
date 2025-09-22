using Microsoft.AspNetCore.Http;
using Shared.Core.Interfaces;
using System.Security.Claims;

namespace Host.WebApi.Web.Providers
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
                throw new NotImplementedException();
            }
        }
    }
}
