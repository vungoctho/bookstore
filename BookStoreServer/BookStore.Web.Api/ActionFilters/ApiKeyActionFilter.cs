using BookStore.Web.Api.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookStore.Web.Api.ActionFilters
{
    public class ApiKeyActionFilter : ActionFilterAttribute
    {
        private readonly IConfiguration _configuration;
        public ApiKeyActionFilter(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            var isValid = true;
            var constApiKey = "ApiKey";
            var configuredApiKey = _configuration.GetValue<string>(constApiKey);
            if (context.HttpContext.Request.Headers.ContainsKey(constApiKey)) {
                isValid = context.HttpContext.Request.Headers[constApiKey].Equals(configuredApiKey);
            }
            if (isValid)
            {
                base.OnActionExecuting(context);
            }
            else
            {
                context.Result = new JsonResult(new UnauthorizationResult());
            }
            
        }
    }
}
