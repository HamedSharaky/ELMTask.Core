using System.Net;
using ELM.Core.Application.Common.Exceptions;
using Newtonsoft.Json;

namespace ELM.Core.Presentation.Configuration.Middlewares
{
    internal class CustomExceptionHandlerMiddleware
    {
        private readonly RequestDelegate _next;

        public CustomExceptionHandlerMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(context, ex);
            }
        }

        private Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)GetResponseStatusCode(exception);

            var err = exception is ValidationException 
                ? JsonConvert.SerializeObject(new { Message = exception.Message, Details = (exception as ValidationException).Failures })
                : JsonConvert.SerializeObject(new { Message = exception.Message });


            return context.Response.WriteAsync(err);
        }

        private static HttpStatusCode GetResponseStatusCode(Exception exception)
        {
            return exception switch
            {
                BusinessRuleValidationException => HttpStatusCode.Conflict,
                NotFoundException => HttpStatusCode.NotFound,
                ValidationException => HttpStatusCode.BadRequest,
                _ => HttpStatusCode.InternalServerError,
            };
        }
    }

    internal static class CustomExceptionHandlerMiddlewareExtensions
    {
        public static IApplicationBuilder UseCustomExceptionHandler(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<CustomExceptionHandlerMiddleware>();
        }
    }
}