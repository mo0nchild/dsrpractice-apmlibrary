﻿using FluentValidation;
using FluentValidation.Results;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APMLibrary.Bll.Common.Behaviors
{
    public partial class ValidationBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
        where TRequest : IRequest<TResponse>
    {
        protected readonly IEnumerable<IValidator<TRequest>> _validators = default!;
        public ValidationBehavior(IEnumerable<IValidator<TRequest>> validators) : base() 
            => this._validators = validators;

        public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, 
            CancellationToken cancellationToken)
        {
            var context = new ValidationContext<TRequest>(request);
            var failures = new List<ValidationFailure>();

            foreach(var validator in this._validators)
            {
                failures.AddRange((await validator.ValidateAsync(context)).Errors);
            }
            if(failures.Count > 0) throw new ValidationException(failures);
            return await next.Invoke();
        }
    }
}
