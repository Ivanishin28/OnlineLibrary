using Shared.Core.Exceptions;
using Shared.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Shared.Core.Extensions
{
    public static class ResultExtensions
    {
        public static Result<T> ToFailure<T>(this Result result)
        {
            if (result.IsSuccess)
            {
                throw new ConvertSuccessToFailuteException();
            }

            return Result<T>.Failure(result.Errors);
        }

        public static Result Combine(params Result[] results)
        {
            if(results.All(result => result.IsSuccess))
            {
                return Result.Success();
            }

            var errors = results
                .SelectMany(result => result.Errors);

            return Result.Failure(errors);
        }
    }
}
