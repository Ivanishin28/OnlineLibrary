using MediatR;
using Shared.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.Contracts.Interfaces
{
    public interface IResultRequest : IRequest<Result>
    {
    }
    
    public interface IResultRequest<T> : IRequest<Result<T>>
    {
    }
}
