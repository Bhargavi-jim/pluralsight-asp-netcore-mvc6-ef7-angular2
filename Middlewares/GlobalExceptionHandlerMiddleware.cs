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

        public async Task Invoke(HttpContext context, ILoggerFactory logger)
        {
            try
            {
                await _next(context);
            }
            catch (Exception)
            {
                logger.AddConsole(LogLevel.Debug, true);
                throw;
            }
        }
    }
}