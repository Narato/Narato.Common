using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Diagnostics;

namespace Narato.Common.ActionFilters
{
    public class ExecutionTimingFilter : ActionFilterAttribute
    {

        public override void OnActionExecuting(ActionExecutingContext context)
        {
            base.OnActionExecuting(context);
            var sw = new Stopwatch();
            sw.Start();
            context.HttpContext.Items.Add("api_timing", sw);
        }

        public override void OnActionExecuted(ActionExecutedContext context)
        {
           base.OnActionExecuted(context);

            var sw = context.HttpContext.Items["api_timing"] as Stopwatch;

            if (sw != null)
            {
                sw.Stop();
                var objectResult = context.Result as ObjectResult;

                if (objectResult != null && objectResult.Value is Models.Response)
                {
                    (objectResult.Value as Models.Response).Generation.Duration = sw.ElapsedMilliseconds;
                    (objectResult.Value as Models.Response).Generation.TimeStamp = DateTime.Now;
                }
            }
        }
    }
}
