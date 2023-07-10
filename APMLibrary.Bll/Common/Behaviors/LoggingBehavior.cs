using iTextSharp.text.log;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APMLibrary.Bll.Common.Behaviors
{
    public partial class LoggingBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
        where TRequest : IRequest<TResponse>
    {
        private readonly ILogger<LoggingBehavior<TRequest, TResponse>> _logger = default!;
        public LoggingBehavior(ILogger<LoggingBehavior<TRequest, TResponse>> logger) : base() 
            => this._logger = logger;

        public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, 
            CancellationToken cancellationToken)
        {
            this._logger.LogInformation($"Request: {typeof(TRequest).Name}");
            return await next.Invoke();
        }
    }
}
