using System.Collections.Immutable;
using System.Runtime.CompilerServices;

namespace Shared.Core.Models
{
    public class Result
    {
        private string[] _errors;
        
        public bool IsSuccess => _errors.Length == 0;
        public bool IsFailure => !IsSuccess;

        public IImmutableList<string> Errors => _errors.ToImmutableArray();
        
        public string? ComposedErrorMessage => !IsSuccess ? String.Join(' ', _errors) : null;

        protected Result(params string[] errors)
        {
            _errors = errors;
        }

        public static Result Success()
        {
            return new Result();
        }

        public static Result Failure(params string[] errors)
        {
            return new Result(errors);
        }
    }

    public class Result<T> : Result
    {
        public T? Model { get; private set; }

        public Result(params string[] errors) : base(errors) { }

        public Result(T? value)
        {
            Model = value;
        }

        public static Result<T> Success(T model)
        {
            return new Result<T>(model);
        }

        public static Result<T> Failure(params string[] errors)
        {
            return new Result<T>(errors);
        }

        public static implicit operator Result<T>(T model)
        {
            return Result<T>.Success(model);
        }
    }
}
