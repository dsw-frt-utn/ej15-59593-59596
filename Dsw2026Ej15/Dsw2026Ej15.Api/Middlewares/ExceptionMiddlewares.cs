using Dsw2026Ej15.Domain.Exceptions;
using Microsoft.AspNetCore.Mvc;

namespace Dsw2026Ej15.Api.Middlewares
{
    public class ExceptionMiddlewares
    {
        private readonly RequestDelegate _next;

        public ExceptionMiddlewares(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            } 
            catch (ValidationException exception)
            {
                context.Response.StatusCode = StatusCodes.Status400BadRequest;
                context.Response.ContentType = "aplication/json";

                await context.Response.WriteAsJsonAsync(new { message = exception.Message });
            }
            catch (Exception)
            {
                context.Response.StatusCode = StatusCodes.Status500InternalServerError;
                context.Response.ContentType = "aplication/problem+json";

                var body = new ProblemDetails
                {
                    Status = StatusCodes.Status500InternalServerError,
                    Title = "Error interno del servidor"
                };

                await context.Response.WriteAsJsonAsync(body);
            }
        }
    }
}
