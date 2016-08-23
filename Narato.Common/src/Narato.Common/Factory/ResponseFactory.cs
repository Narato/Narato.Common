using Narato.Common.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Narato.Common.Exceptions;
using Narato.Common.Interfaces;
using System.Net;
using System.Reflection;
using Narato.Common.ActionResult;

namespace Narato.Common.Factory
{
    public class ResponseFactory : IResponseFactory
    {
        private IExceptionHandler _exceptionHandler;

        public ResponseFactory(IExceptionHandler exceptionHandler)
        {
            _exceptionHandler = exceptionHandler;
        }
        //private string GetInnerMostMessage(Exception e)
        //{
        //    if (e.InnerException == null)
        //    {
        //        return e.Message;
        //    }
        //    return GetInnerMostMessage(e.InnerException);
        //}

        public IActionResult CreateGetResponse<T>(Func<T> callback, string absolutePath)
        {
            var feedback = new List<FeedbackItem>();
            T returnData = default(T);

            try
            {
                returnData = _exceptionHandler.PrettifyExceptions<T>(callback);
                if (returnData != null)
                    return new ObjectResult(new Response<T>(returnData, absolutePath));
                return new NotFoundObjectResult(new Response<T>(new FeedbackItem() { Description = "The object was not found", Type = FeedbackType.Info }, absolutePath));
            }
            catch (ValidationException e)
            {
                var response = new Response<T>(e.Feedback, absolutePath);
                return new BadRequestObjectResult(response);
            }
            catch (EntityNotFoundException e)
            {
                if (! e.MessageSet)
                {
                    return new NotFoundResult();
                }

                var response = new Response<T>(new FeedbackItem { Description = e.Message, Type = FeedbackType.Error }, absolutePath);
                return new NotFoundObjectResult(response);
            }
            catch (UnauthorizedAccessException e)
            {
                return new UnauthorizedResult();
            }
            catch (ExceptionWithFeedback e)
            {
                var response = new Response<T>(e.Feedback, absolutePath);
                return new InternalServerErrorWithResponse(response);
            }
            catch (Exception e)
            {
                var response = new Response<T>(returnData, new FeedbackItem { Description = e.Message, Type = FeedbackType.Error }, absolutePath);
                return new InternalServerErrorWithResponse(response);
            }
        }

        public IActionResult CreatePostResponse<T>(Func<T> callback, string absolutePath, string routeName, object routeValues, List<RouteValuesIdentifierPair> routeValueIdentifierPairs = null)
        {
            var feedback = new List<FeedbackItem>();

            try
            {
                var returndata = _exceptionHandler.PrettifyExceptions<T>(callback);

                if (routeValueIdentifierPairs != null)
                {
                    routeValueIdentifierPairs.ForEach(x =>
                    {
                        var propertyInfo = returndata.GetType().GetProperty(x.ModelIdentifier);
                        var modelIdentiefer = propertyInfo.GetValue(returndata);

                        var routePropertyInfo = routeValues.GetType().GetProperty(x.RouteValuesIdentifier);
                        routePropertyInfo.SetValue(routeValues, modelIdentiefer);
                    });
                }

                return new CreatedAtRouteResult(routeName, routeValues, new Response<T>(returndata, absolutePath));
            }
            catch (ValidationException e)
            {
                var response = new Response<T>(e.Feedback, absolutePath);
                return new BadRequestObjectResult(response);
            }
            catch (EntityNotFoundException e)
            {
                if (! e.MessageSet)
                {
                    return new NotFoundResult();
                }

                var response = new Response<T>(new FeedbackItem { Description = e.Message, Type = FeedbackType.Error }, absolutePath);
                return new NotFoundObjectResult(response);
            }
            catch (UnauthorizedAccessException e)
            {
                return new UnauthorizedResult();
            }
            catch (ExceptionWithFeedback e)
            {
                var response = new Response<T>(e.Feedback, absolutePath);
                return new InternalServerErrorWithResponse(response);
            }
            catch (Exception e)
            {
                var response = new Response<T>(new FeedbackItem { Description = e.Message, Type = FeedbackType.Error }, absolutePath);
                return new InternalServerErrorWithResponse(response);
            }
        }

        public IActionResult CreatePutResponse<T>(Func<T> callback, string absolutePath)
        {
            var feedback = new List<FeedbackItem>();

            try
            {
                var returndata = _exceptionHandler.PrettifyExceptions<T>(callback);
                return new OkObjectResult(new Response<T>(returndata, absolutePath));
            }
            catch (ValidationException e)
            {
                var response = new Response<T>(e.Feedback, absolutePath);
                return new BadRequestObjectResult(response);
            }
            catch (EntityNotFoundException e)
            {
                if (!e.MessageSet)
                {
                    return new NotFoundResult();
                }

                var response = new Response<T>(new FeedbackItem { Description = e.Message, Type = FeedbackType.Error }, absolutePath);
                return new NotFoundObjectResult(response);
            }
            catch (UnauthorizedAccessException e)
            {
                return new UnauthorizedResult();
            }
            catch (ExceptionWithFeedback e)
            {
                var response = new Response<T>(e.Feedback, absolutePath);
                return new InternalServerErrorWithResponse(response);
            }
            catch (Exception e)
            {
                var response = new Response<T>(new FeedbackItem { Description = e.Message, Type = FeedbackType.Error }, absolutePath);
                return new InternalServerErrorWithResponse(response);
            }
        }

        public IActionResult CreateDeleteResponse(Action callback, string absolutePath)
        {
            var feedback = new List<FeedbackItem>();
            try
            {
                callback();
                return new NoContentResult();
            }
            catch (UnauthorizedAccessException)   
            {
                return new UnauthorizedResult();
            }
            catch (ValidationException e)
            {
                var response = new Response(e.Feedback);
                return new BadRequestObjectResult(response);
            }
            catch (EntityNotFoundException e)
            {
                if (! e.MessageSet)
                {
                    return new NotFoundResult();
                }
                var response = new Response(new FeedbackItem { Description = e.Message, Type = FeedbackType.Error });
                return new NotFoundObjectResult(response);
            }
            catch (ExceptionWithFeedback e)
            {
                var response = new Response(e.Feedback);
                return new InternalServerErrorWithResponse(response);
            }
            catch (Exception e)
            {
                var response = new Response(new FeedbackItem { Description = e.Message, Type = FeedbackType.Error });
                return new InternalServerErrorWithResponse(response);
            }
        }

        public IActionResult CreateGetResponseForCollection<T>(Func<PagedCollectionResponse<IEnumerable<T>>> callback, string absolutePath)
        {
            try
            {
                var returnData = _exceptionHandler.PrettifyExceptions(callback);

                var response = new Response<IEnumerable<T>>(returnData, absolutePath);

                return new ObjectResult(returnData);
            }
            catch (ValidationException e)
            {
                var response = new Response<T>(e.Feedback, absolutePath);
                return new BadRequestObjectResult(response);
            }
            catch (EntityNotFoundException e)
            {
                if (! e.MessageSet)
                {
                    return new NotFoundResult();
                }

                var response = new Response<T>(new FeedbackItem { Description = e.Message, Type = FeedbackType.Error }, absolutePath);
                return new NotFoundObjectResult(response);
            }
            catch (UnauthorizedAccessException e)
            {
                return new UnauthorizedResult();
            }
            catch (ExceptionWithFeedback e)
            {
                var response = new Response<T>(e.Feedback, absolutePath);
                return new InternalServerErrorWithResponse(response);
            }
            catch (Exception e)
            {
                var response = new Response<T>(new FeedbackItem { Description = e.Message, Type = FeedbackType.Error }, absolutePath);
                return new InternalServerErrorWithResponse(response);
            }
        }

        public IActionResult CreateMissingParam(List<MissingParam> missingParams)
        {
            var feedbackItems = new List<FeedbackItem>();

            foreach (var param in missingParams)
            {
                switch (param.Type)
                {
                    case MissingParamType.QuerystringParam:
                        feedbackItems.Add(new FeedbackItem() { Type = FeedbackType.Error, Description = $"Parameter '{param.Name}' is a required querystring parameter" });
                        break;
                    case MissingParamType.Body:
                        feedbackItems.Add(new FeedbackItem() { Type = FeedbackType.Error, Description = $"The request is missing a correct body ({param.Name})" });
                        break;
                    case MissingParamType.Header:
                        feedbackItems.Add(new FeedbackItem() { Type = FeedbackType.Error, Description = $"The request is missing a header value for ({param.Name})" });
                        break;
                }
            }
            var response = new Response(feedbackItems);

            return new BadRequestObjectResult(response);
        }

        public IActionResult CreateMissingParam(MissingParam missingParam)
        {
            return CreateMissingParam(new List<MissingParam> { missingParam });
        }
    }
}
