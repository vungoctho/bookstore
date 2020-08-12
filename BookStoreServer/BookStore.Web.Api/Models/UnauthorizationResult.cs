using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookStore.Web.Api.Models
{
    public class UnauthorizationResult : BaseResult
    {
        public UnauthorizationResult()
        {
            this.StatusCode = StatusCodes.Status401Unauthorized;
            this.Message = "Invalid ApiKey";
        }
    }
}
