using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Narato.Common.Models;

namespace Narato.Common.ActionFilters
{
    public class ModelValidationFilter : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            if (!context.ModelState.IsValid)
            {
                var response = new Response();
                AddValidationFeedback(context.ModelState, response);
                context.Result = new BadRequestObjectResult(response);
            }
        }

        public static void AddValidationFeedback(ModelStateDictionary modelState, Response response)
        {
            foreach (var modelstateItem in modelState)
            {
                foreach (var error in modelstateItem.Value.Errors)
                {
                    var errorMessage = error.ErrorMessage;
                    if (string.IsNullOrEmpty(errorMessage))
                        errorMessage = error.Exception.Message;
                    response.Feedback.Add(new FeedbackItem
                    {
                        Type = FeedbackType.Error,
                        Description = $"{modelstateItem.Key}: {errorMessage}"
                    });
                }
            }
        }
    }
}