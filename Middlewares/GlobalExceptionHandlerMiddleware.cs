/*
 * Middleware that returns a nicely formatted and friendly message to the UI.
 */

using System;
using System.Threading.Tasks;
using Microsoft.AspNet.Builder;
using Microsoft.AspNet.Http;
using Microsoft.Extensions.Logging;

namespace MyWorld.Middlewares
{
    public class GlobalExceptionHandlerMiddleware
    {
        private readonly RequestDelegate _next;

        public GlobalExceptionHandlerMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        // ToDo:: Format and return nice message to users. 
        public async Task Invoke(HttpContext context, ILoggerFactory logger)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                //if(ex is ...)
                // else
//                {
                    //Response.StatusCode = (int)HttpStatusCode.BadRequest;
//                }
                logger.AddConsole(LogLevel.Debug, true);                

                throw;
            }
        }
    }
}