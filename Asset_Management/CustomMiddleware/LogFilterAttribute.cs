using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Diagnostics;

namespace Asset_Management.CustomMiddleware
{
    public class LogFilterAttribute : ActionFilterAttribute
    {
        private Stopwatch stopwatch;
        private readonly ILogger _logger;

        public LogFilterAttribute(ILoggerFactory loggerFactory)
        {
            stopwatch = new Stopwatch();
            _logger = loggerFactory.CreateLogger<LogFilterAttribute>();
            //this.logger = logger;
        }

        private void LogRequest(string currentStatus,RouteData data)
        {
            string controllerName = data.Values["controller"].ToString();
            string actionName = data.Values["action"].ToString();
            string logMsg = $"Current Status of Request is {currentStatus} in {actionName} Action Method of {controllerName} contrller";
            _logger.LogInformation(logMsg);
        }
        
        public override void OnActionExecuting(ActionExecutingContext context)
        {
           stopwatch.Start();
            _logger.LogInformation("On Action Executing");
            LogRequest("On Action Executing", context.RouteData);
        }

        public override void OnActionExecuted(ActionExecutedContext context)
        {
            stopwatch.Stop();
            _logger.LogInformation("On Action Executed");
            LogRequest("On Action Executed", context.RouteData);
            _logger.LogInformation($"time taken to execute api is {stopwatch.ElapsedMilliseconds} ms ");
            stopwatch.Reset();
            
        }
    }
}
