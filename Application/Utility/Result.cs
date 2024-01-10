using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Utility
{
    public class Result<T>
    {
        public bool Success { get; set; }
        public T Value { get; set; }
        public ErrorCode Error { get; set; }
        public string Message { get; set; }

        public static Result<T> SuccessResult(T value) => new Result<T> { Success = true, Value = value };
        public static Result<T> Failure(string error,ErrorCode errorCode) => new Result<T> { Success = false, Error = errorCode, Message = error };
    }
    public enum ErrorCode 
    {
       GeneralError,
       Unauthorized,
       None
    }
}
