using GraphReview.Domain.Exceptions.Base;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Text.Json;

namespace GraphReview.Api.Middlewares
{
    public class GlobalExceptionHandlingMiddleware : IMiddleware
    {
        //private readonly ILogger _logger;

        //public GlobalExceptionHandlingMiddleware(ILogger logger)
        //{
        //    _logger = logger;
        //}

        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            try
            {
                await next(context);
            }
            catch (Exception ex)
            {
                await HandleAsync(context, ex);
            }
        }

        public async Task HandleAsync(HttpContext context, Exception ex)
        {
            //_logger.LogError(ex, ex.Message, ex.StackTrace);

            if (ex is NotFoundException)
            {
                context.Response.StatusCode = (int)HttpStatusCode.NotFound;
            }
            else
            {
                context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
            }            

            var problem = new ProblemDetails()
            {
                Status = context.Response.StatusCode,
                Type = "Server Error",
                Title = ex.Message,
                Detail = ex.StackTrace,
            };

            string json = JsonSerializer.Serialize(problem);

            context.Response.ContentType = "application/json";
            await context.Response.WriteAsync(json);
        }
    }
}
