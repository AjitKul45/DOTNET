using Asset_Management.CustomMiddleware;
using System.Runtime.CompilerServices;

namespace Asset_Management
{
    public record ErrorResponse
    {
        public int StatucCode { get; set; }
        public string Message { get; set; }
    }

    public class ExceptionMiddlewre
    {
        RequestDelegate _next;
        private readonly ILogger _logger;


        public ExceptionMiddlewre(RequestDelegate next, ILoggerFactory loggerFactory)
        {
            _next = next;
            _logger = loggerFactory.CreateLogger<ExceptionMiddlewre>();

        }
        /// <summary>
        /// Write the logic for Custom Middleware Here 
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                // If No Exception COntinue with next middleware in pipeline
                await _next(context);
            }
            catch (Exception ex)
            {
                // Handle the Exception and Generate Http Response
                // 1. Read the Error MEssage
                string message = ex.Message;
                // 1.a. Define a Error COde for Response Here
                context.Response.StatusCode = StatusCodes.Status500InternalServerError;
                // 2. STore this information into the record
                _logger.LogError(message);
                var errorResponse = new ErrorResponse()
                {
                    StatucCode = context.Response.StatusCode,
                    Message = message
                };
                // 3. Write the response as JSON
                await context.Response.WriteAsJsonAsync<ErrorResponse>(errorResponse);
            }
        }
    }

    public static class CustomExceptionMiddleare
    {
        public static void UseAppException(this IApplicationBuilder builder)
        {
            builder.UseMiddleware<ExceptionMiddlewre>();
        }
    }

}
