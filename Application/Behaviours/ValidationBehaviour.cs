﻿using FluentValidation;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Behaviours
{
    public class ValidationBehaviour<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    where TRequest : IRequest<TResponse>
    {
        private readonly IEnumerable<IValidator<TRequest>> _validators;

        public ValidationBehaviour(IEnumerable<IValidator<TRequest>> validators)
        {
            _validators = validators;
        }



        public Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
        {
            var _context = new ValidationContext<TRequest>(request);
            var failures = _validators
                .Select(v => v.Validate(_context))
                .SelectMany(result => result.Errors)
                .Where(f => f != null)
                
                .ToList();


            if (failures.Any())
            {
                throw new ValidationException("Validation failed", failures);
            }

            return next();
        }
    }
}
