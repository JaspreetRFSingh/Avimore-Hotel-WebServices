using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Hosting;
using Northstar.WS.Models;

namespace Northstar.WS.Filters
{
    //a filter is a chunk of code that runs before or after ASP.NET core processes a request
    //Filters that handle errors and exceptions are called Exception Filters
    public class JsonExceptionFilter : IExceptionFilter
    {

        private readonly IWebHostEnvironment _env;

        public JsonExceptionFilter(IWebHostEnvironment env)
        {
            _env = env;
        }

        public void OnException(ExceptionContext context)
        {
            var error = new ApiError();
            if (_env.IsDevelopment())
            {
                error.Message = context.Exception.Message;
            }
            else
            {
                error.Message = "A simple error occured";
            }
            error.code = 301;
            context.Result = new ObjectResult(error)
            {
                StatusCode = 500
            };
        }
         
    }
}
