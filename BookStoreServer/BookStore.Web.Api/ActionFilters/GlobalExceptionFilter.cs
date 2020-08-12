using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Logging;
using System;

namespace BookStore.Web.Api.ActionFilters
{
    public class GlobalExceptionFilter : ActionFilterAttribute
    {
        private readonly ILogger<GlobalExceptionFilter> _logger;
        public GlobalExceptionFilter(IServiceProvider serviceProvider)
        {
            var logFactory = (ILoggerFactory)serviceProvider.GetService(typeof(ILoggerFactory));
            _logger = logFactory.CreateLogger<GlobalExceptionFilter>();
        }

        public override void OnActionExecuted(ActionExecutedContext context)
        {
            if(context.Exception != null)
            {
                _logger.LogError(context.Exception, context.Exception.Message);
            }
            base.OnActionExecuted(context);
        }
    }
}
