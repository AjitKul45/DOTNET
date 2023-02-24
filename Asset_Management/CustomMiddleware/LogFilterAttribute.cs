using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Diagnostics;

namespace Asset_Management.CustomMiddleware
{
    public class LogFilterAttribute : ActionFilterAttribute
    {
        private Stopwatch stopwatch;
        //private readonly ILogger<LogFilterAttribute> logger;

        public LogFilterAttribute(ILogger<LogFilterAttribute> logger)
        {
            stopwatch = new Stopwatch(); 
            //this.logger = logger;
        }

        private void LogRequest(string currentStatus,RouteData data)
        {
            string controllerName = data.Values["controller"].ToString();
            string actionName = data.Values["action"].ToString();
            string logMsg = $"Current Status of Request is {currentStatus} in {actionName} Action Method of {controllerName} contrller";

            Debug.WriteLine(logMsg);
        }
        
        public override void OnActionExecuting(ActionExecutingContext context)
        {
           stopwatch.Start();
            LogRequest("On Action Executing", context.RouteData);
        }

        public override void OnActionExecuted(ActionExecutedContext context)
        {
            stopwatch.Stop();
            LogRequest("On Action Executed", context.RouteData);
            Debug.WriteLine($"time taken to execute api is {stopwatch.ElapsedMilliseconds} ms ");
            //logger.LogInformation("logger called");
            stopwatch.Reset();
        }
    }
}
