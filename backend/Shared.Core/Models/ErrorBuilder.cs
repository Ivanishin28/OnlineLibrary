using Shared.Core.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.Core.Models
{
    public class ErrorBuilder
    {
        private List<string> ProcessedCodes = new List<string>();
        public string ErrorCodeBase { get; private set; }

        public ErrorBuilder(string errorCodeBase)
        {
            ErrorCodeBase = errorCodeBase;
        }

        public Error BuildError(string code, string message)
        {
            if (String.IsNullOrEmpty(code) || code == "")
            {
                throw new InvalidErrorCodeException();
            }

            if (ProcessedCodes.Contains(code))
            {
                throw new DuplicateErrorCodeException();
            }

            ProcessedCodes.Add(code);

            return new Error($"{ErrorCodeBase}.{code}", message);
        }
    }
}
