using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookStore.Web.Api.Models
{
    public class BaseResult
    {
        public int StatusCode { get; set; }
        public string Message { get; set; }
    }
}
