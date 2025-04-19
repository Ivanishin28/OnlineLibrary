using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Models
{
    public class Result
    {
        protected string[] _errors;
        public bool IsSuccess => _errors.Length == 0;
        public string? ErrorMessage => !IsSuccess ? String.Join(' ', _errors) : null;

        protected Result() { }

        public static Result Success()
        {
            return new Result();
        }

        public static Result Failure(params string[] errors)
        {
            var result = new Result();
            result._errors = errors;
            return result;
        }
    }

    public class Result<T> : Result
    {
        public T Value { get; private set; }

        public static Result<T> Success(T model)
        {
            var result = new Result<T>();
            result.Value = model;
            return result;
        }

        public static Result<T> Failure(params string[] errors)
        {
            var result = new Result<T>();
            result._errors = errors;
            return result;
        }
    }
}
