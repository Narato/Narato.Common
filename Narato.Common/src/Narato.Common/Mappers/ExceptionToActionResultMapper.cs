using Narato.Common.Interfaces;
using System;
using Microsoft.AspNetCore.Mvc;
using Narato.Common.Exceptions;
using Narato.Common.Models;
using Narato.Common.ActionResult;

namespace Narato.Common.Mappers
{
    public class ExceptionToActionResultMapper : IExceptionToActionResultMapper
    {
        public IActionResult Map<T>(Exception ex, string absolutePath)
        {
            if (ex is ValidationException)
            {
                var typedEx = ex as ValidationException;
                var response = new Response<T>(typedEx.Feedback, absolutePath, 400);
                response.Identifier = typedEx.GetTrackingGuid();
                return new BadRequestObjectResult(response);
            }

            if (ex is EntityNotFoundException)
            {
                var typedEx = ex as EntityNotFoundException;
                if (!typedEx.MessageSet)
                {
                    return new NotFoundResult();
                }

                var response = new Response<T>(new FeedbackItem { Description = typedEx.Message, Type = FeedbackType.Error }, absolutePath, 404);
                response.Identifier = typedEx.GetTrackingGuid();
                return new NotFoundObjectResult(response);
            }

            if (ex is UnauthorizedException)
            {
                return new UnauthorizedResult();
            }

            if (ex is ForbiddenException)
            {
                return new ForbidResult();
            }

            if (ex is ExceptionWithFeedback)
            {
                var typedEx = ex as ExceptionWithFeedback;
                var response = new Response<T>(typedEx.Feedback, absolutePath, 500);
                response.Identifier = typedEx.GetTrackingGuid();
                return new InternalServerErrorWithResponse(response);
            }

            var message = "Something went wrong. Contact support and give them the identifier found below.";
            // if development ==> expose exception message
            if (Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") != null && Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT").ToLower().Equals("development"))
            {
                message = ex.Message;
            }
            // catch all (just Exception)
            var catchAllResponse = new Response<T>(new FeedbackItem { Description = message, Type = FeedbackType.Error }, absolutePath, 500);
            catchAllResponse.Identifier = ex.GetTrackingGuid();
            return new InternalServerErrorWithResponse(catchAllResponse);
        }
    }
}
