using IoT.IncidentManagement.Application.Exceptions;

using Microsoft.AspNetCore.Http;

using Newtonsoft.Json;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace IoT.IncidentManagement.Api.Middleware
{
    public class ExceptionHandlerMiddleware
    {
        private readonly RequestDelegate _next;

        public ExceptionHandlerMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch(Exception ex)
            {
                 await HandleException(context, ex);
            }
        }


        private static Task HandleException(HttpContext context, Exception exception)
        {

            context.Response.ContentType = "application/json";

            (HttpStatusCode httpStatusCode, string message) = exception switch
            {
                ValidationException validationException => (HttpStatusCode.BadRequest, JsonConvert.SerializeObject(validationException.Errors)),
                BadRequestException badRequestException => (HttpStatusCode.BadRequest, JsonConvert.SerializeObject(badRequestException.Message)),
                NotFoundException notFoundException => (HttpStatusCode.NotFound, JsonConvert.SerializeObject(notFoundException.Message)),
                _ => (HttpStatusCode.InternalServerError, JsonConvert.SerializeObject(new { error = exception.Message })),
            };

            context.Response.StatusCode = (int)httpStatusCode;

            return context.Response.WriteAsync(message);
        }
    }
}
