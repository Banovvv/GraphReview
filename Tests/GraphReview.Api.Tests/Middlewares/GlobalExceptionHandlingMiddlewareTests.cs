using FluentAssertions;
using GraphReview.Api.Middlewares;
using Microsoft.AspNetCore.Http;
using System.Net;

namespace GraphReview.Api.Tests.Middlewares
{
    public class GlobalExceptionHandlingMiddlewareTests
    {
        private readonly GlobalExceptionHandlingMiddleware _middleware;

        public GlobalExceptionHandlingMiddlewareTests()
        {
            _middleware = new GlobalExceptionHandlingMiddleware();
        }

        [Fact]
        public async Task GivenThatExceptionIsThrown_WhenInvokeAsyncIsInvoked_ThenProblemResponseIsRetured()
        {
            // Arange
            var context = new DefaultHttpContext();
            var exception = new ArgumentNullException();

            static Task requestDelegate(HttpContext HttpContext)
            {
                return Task.FromException(new ArgumentNullException());
            }

            // Act
            await _middleware.InvokeAsync(context, requestDelegate);

            // Assert
            context.Response.ContentType.Should().Be("application/json");
            context.Response.StatusCode.Should().Be((int)HttpStatusCode.InternalServerError);
        }

        [Fact]
        public async Task GivenValidParameters_WhenHandleAsyncIsInvoked_ThenProblemResponseIsRetured()
        {
            // Arange
            var context = new DefaultHttpContext();
            var exception = new ArgumentNullException();

            // Act
            await _middleware.HandleAsync(context, exception);

            // Assert
            context.Response.ContentType.Should().Be("application/json");
            context.Response.StatusCode.Should().Be((int)HttpStatusCode.InternalServerError);
        }
    }
}
