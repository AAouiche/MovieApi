using System.ComponentModel.DataAnnotations;
using System.Security.Authentication;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;

namespace MovieApi.Middleware
{
    public class ExceptionHandlingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ExceptionHandlingMiddleware> _logger;

        public ExceptionHandlingMiddleware(RequestDelegate next, ILogger<ExceptionHandlingMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception e)
            {
                _logger.LogError(e, "An error occurred while processing your request.");
                var exceptionDetails = GetExceptionDetails(e);

                var problemDetails = new ProblemDetails
                {
                    Status = exceptionDetails.StatusCode,
                    Type = exceptionDetails.Type,

                };

                if(exceptionDetails.Errors != null)
                {
                    problemDetails.Extensions["Errors"] = exceptionDetails.Errors;
                }

                context.Response.StatusCode = (int)problemDetails.Status;

                await context.Response.WriteAsJsonAsync(problemDetails);
                
            }
        }
        private static ExceptionResponse GetExceptionDetails(Exception e)
        {
            if (e is FluentValidation.ValidationException validationException)
            {
                var errors = validationException.Errors.Select(failure => failure.ErrorMessage).ToList(); 
                return new ExceptionResponse(
                 StatusCodes.Status400BadRequest,
                 "ValidationError",
                 errors
              );
            }
            
            else
            {
                return new ExceptionResponse(
                    StatusCodes.Status500InternalServerError,
                    "ServerError",
                    null);
            }
        }
    }

    
    internal record ExceptionResponse(int StatusCode, string Type, List<string> Errors);
}
