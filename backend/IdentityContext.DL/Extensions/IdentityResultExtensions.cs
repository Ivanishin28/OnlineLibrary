using Microsoft.AspNetCore.Identity;
using Shared.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IdentityContext.DL.Extensions
{
    public static class IdentityResultExtensions
    {
        public static Result ToResult(this IdentityResult identityResult)
        {
            if (identityResult.Succeeded)
            {
                return Result.Success();
            }

            var error = new Error(
                "Identity", 
                String.Join('.', identityResult.Errors));

            return Result.Failure(error);
        }
    }
}
