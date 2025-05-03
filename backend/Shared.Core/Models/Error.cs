using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.Core.Models
{
    public class Error
    {
        public string Code { get; private set; }
        public string Message { get; private set; }

        public Error(string code, string message)
        {
            Code = code;
            Message = message;
        }
    }
}
