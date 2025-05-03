using Shared.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookContext.Application.Models
{
    public class ApiResponse<T>
    {
        public T Data { get; private set; }
        public Error[] Errors { get; private set; }
        
        public ApiResponse(T data, Error[] errors)
        {
            Data = data;
            Errors = errors;
        }
    }
}
