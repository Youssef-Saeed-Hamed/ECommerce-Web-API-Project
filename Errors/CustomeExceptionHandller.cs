using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Net;
using System.Text.Json;
using System.Threading.Tasks;
namespace E_Commerce_Website_Persentation.Errors
{
    public class CustomeExceptionHandller 
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<CustomeExceptionHandller> _logger;
        private readonly IHostEnvironment _environment;
        public CustomeExceptionHandller(RequestDelegate next, ILogger<CustomeExceptionHandller> logger, IHostEnvironment environment)
        {
            _next = next;
            _logger = logger;
            _environment = environment;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next.Invoke(context);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
               // context.Response.ContentType = "application/json";
                var response = _environment.IsDevelopment() ? new ApiExceptionResponse(
                    (int)HttpStatusCode.InternalServerError, ex.Message, ex.StackTrace) :
                    new ApiExceptionResponse((int) HttpStatusCode.InternalServerError);

                
                 //var json = JsonSerializer.Serialize(Response, new JsonSerializerOptions
                //{
                //    PropertyNamingPolicy = JsonNamingPolicy.CamelCase
                //});

               // await context.Response.WriteAsync(json);
               await context.Response.WriteAsJsonAsync(response);
            }
        }
    }
}
