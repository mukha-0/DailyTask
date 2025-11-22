using System.Net;
using System.Text.Json;
using DailyTask.Service.Exceptions;
using Serilog;
using static System.Net.Mime.MediaTypeNames;

namespace ExamProject.Middlewares
{
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate next;
        private readonly ILogger<ExceptionMiddleware> logger;

        public ExceptionMiddleware(RequestDelegate next, ILogger<ExceptionMiddleware> logger)
        {
            this.next = next;
            this.logger = logger;
        }

        public async Task InvokeAsync(HttpContext httpContext)
        {
            try
            {
                await next.Invoke(httpContext);
            }
            catch (NotFoundException ex)
            {
                await httpContext.Response.WriteAsJsonAsync(new ErrorResponse
                {
                    StatusCode = ex.StatusCode,
                    Message = ex.Message
                });
            }
            catch (AlreadyExistException ex)
            {
                await httpContext.Response.WriteAsJsonAsync(new ErrorResponse
                {
                    StatusCode = ex.StatusCode,
                    Message = ex.Message
                });
            }
            catch (ArgumentIsNotValidException ex)
            {
                await httpContext.Response.WriteAsJsonAsync(new ErrorResponse
                {
                    StatusCode = ex.StatusCode,
                    Message = ex.Message
                });
            }
            catch (Exception ex)
            {
                await httpContext.Response.WriteAsJsonAsync(new ErrorResponse
                {
                    StatusCode = 500,
                    Message = "Server error occurred",
                });

                logger.LogError(ex, ex.Message);
            }
        }
    }

    public class ErrorResponse
    {
        public int StatusCode { get; set; }
        public string Message { get; set; }
    }
}
