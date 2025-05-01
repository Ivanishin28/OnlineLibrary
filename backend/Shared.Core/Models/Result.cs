using Shared.Core.Exceptions;
using System.Collections.Immutable;
using System.Runtime.CompilerServices;

namespace Shared.Core.Models
{
    public class Result
    {
        private Error[] _errors;
        
        public bool IsSuccess => _errors.Length == 0;
        public bool IsFailure => !IsSuccess;

        public IImmutableList<Error> Errors => _errors.ToImmutableArray();
        
        protected Result(params Error[] errors)
        {
            _errors = errors;
        }

        public Result<T> ToFailure<T>()
        {
            if(IsSuccess)
            {
                throw new ConvertSuccessToFailuteException();
            }

            return Result<T>.Failure(Errors);
        }

        public static Result Success()
        {
            return new Result();
        }

        public static Result Failure(Error errors)
        {
            return new Result(errors);
        }

        public static Result Failure(Error[] errors)
        {
            return new Result(errors);
        }

        public static Result Failure(IEnumerable<Error> errors)
        {
            return new Result(errors.ToArray());
        }
    }

    public class Result<T> : Result
    {
        public T? Model { get; private set; }

        public Result(params Error[] errors) : base(errors) { }

        public Result(T? value)
        {
            Model = value;
        }

        public static Result<T> Success(T model)
        {
            return new Result<T>(model);
        }
        public static Result<T> Failure(Error errors)
        {
            return new Result<T>(errors);
        }

        public static Result<T> Failure(Error[] errors)
        {
            return new Result<T>(errors);
        }

        public static Result<T> Failure(IEnumerable<Error> errors)
        {
            return new Result<T>(errors.ToArray());
        }

        public static implicit operator Result<T>(T model)
        {
            return Result<T>.Success(model);
        }
    }
}
