using Narato.Common.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Narato.Common.Exceptions;
using Narato.Common.Interfaces;

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
                return new NotFoundObjectResult(new Response<T>(new FeedbackItem() { Description="The object was not found", Type=FeedbackType.Info}, absolutePath));
                }
            //catch (ValidatorException ve)
            //{
            ////_logger.Error(new ErrorLogInfo(LayerEnum.FacadeApi, this.GetType().Name, ve));
            ////_ailogger.Error("Validation didn't pass", ve);
            ////statusCode = HttpStatusCode.BadRequest;
            //return new Response<T>
            //{
            //    Feedback = new List<FeedbackItem>() { new FeedbackItem { StatusCode = HttpStatusCode.BadRequest, Description = ve.Message, Type = FeedbackType.ValidationError } },
            //    Self = absolutePath,
            //    Data = returnData,
            //    StatusCode = statusCode
            //};
            //feedback.Add(new FeedbackItem { StatusCode = HttpStatusCode.BadRequest, Description = ve.Message, Type = FeedbackType.ValidationError });
            //}
            //catch (EntityNotFoundException enfe)
            //{
            //    //_logger.Error(new ErrorLogInfo(LayerEnum.FacadeApi, this.GetType().Name, enfe));
            //    //_ailogger.Error(enfe.Message, enfe);
            //    statusCode = HttpStatusCode.NotFound;
            //    feedback.Add(new FeedbackItem { StatusCode = HttpStatusCode.NotFound, Description = enfe.Message, Type = FeedbackType.Warning });
            //}
            catch (UnauthorizedAccessException e)
            {
                return new UnauthorizedResult();
            }
            catch (ExceptionWithFeedback e)
            {
                var response = new Response<T>(e.Feedback, absolutePath);
                return new BadRequestObjectResult(response);
            }
            catch (Exception e)
                {
                    var response = new Response<T>(returnData, new FeedbackItem { Description = e.Message, Type = FeedbackType.ValidationError }, absolutePath);
                    return new BadRequestObjectResult(response);
                }
            }

        public IActionResult CreatePostResponse<T>(Func<T> callback, string absolutePath, string routeName, object routeValues)
        {
            var feedback = new List<FeedbackItem>();

            try
            {
                var returndata = _exceptionHandler.PrettifyExceptions<T>(callback);
                return new CreatedAtRouteResult(routeName, routeValues, new Response<T>(returndata, absolutePath));
            }
            //catch (ValidatorException ve)
            //{
            ////_logger.Error(new ErrorLogInfo(LayerEnum.FacadeApi, this.GetType().Name, ve));
            ////_ailogger.Error("Validation didn't pass", ve);
            ////statusCode = HttpStatusCode.BadRequest;
            //return new Response<T>
            //{
            //    Feedback = new List<FeedbackItem>() { new FeedbackItem { StatusCode = HttpStatusCode.BadRequest, Description = ve.Message, Type = FeedbackType.ValidationError } },
            //    Self = absolutePath,
            //    Data = returnData,
            //    StatusCode = statusCode
            //};
            //feedback.Add(new FeedbackItem { StatusCode = HttpStatusCode.BadRequest, Description = ve.Message, Type = FeedbackType.ValidationError });
            //}
            //catch (EntityNotFoundException enfe)
            //{
            //    //_logger.Error(new ErrorLogInfo(LayerEnum.FacadeApi, this.GetType().Name, enfe));
            //    //_ailogger.Error(enfe.Message, enfe);
            //    statusCode = HttpStatusCode.NotFound;
            //    feedback.Add(new FeedbackItem { StatusCode = HttpStatusCode.NotFound, Description = enfe.Message, Type = FeedbackType.Warning });
            //}
            catch (UnauthorizedAccessException e)
            {
                return new UnauthorizedResult();
            }
            catch (ExceptionWithFeedback e)
            {
                var response = new Response<T>(e.Feedback, absolutePath);
                return new BadRequestObjectResult(response);
            }
            catch (Exception e)
            {
                var response = new Response<T>(new FeedbackItem { Description = e.Message, Type = FeedbackType.ValidationError }, absolutePath);
                return new BadRequestObjectResult(response);
            }
        }

        public IActionResult CreateGetResponseForCollection<T>(Func<IEnumerable<T>> callback, string absolutePath)
            {
                var feedback = new List<FeedbackItem>();
                IEnumerable<T> returnData = new List<T>();

                try
                {
                    returnData = _exceptionHandler.PrettifyExceptions<T>(callback);
                    return new ObjectResult(new Response<IEnumerable<T>>(returnData, absolutePath));
                }
            //catch (ValidatorException ve)
            //{
            ////_logger.Error(new ErrorLogInfo(LayerEnum.FacadeApi, this.GetType().Name, ve));
            ////_ailogger.Error("Validation didn't pass", ve);
            ////statusCode = HttpStatusCode.BadRequest;
            //return new Response<T>
            //{
            //    Feedback = new List<FeedbackItem>() { new FeedbackItem { StatusCode = HttpStatusCode.BadRequest, Description = ve.Message, Type = FeedbackType.ValidationError } },
            //    Self = absolutePath,
            //    Data = returnData,
            //    StatusCode = statusCode
            //};
            //feedback.Add(new FeedbackItem { StatusCode = HttpStatusCode.BadRequest, Description = ve.Message, Type = FeedbackType.ValidationError });
            //}
            //catch (EntityNotFoundException enfe)
            //{
            //    //_logger.Error(new ErrorLogInfo(LayerEnum.FacadeApi, this.GetType().Name, enfe));
            //    //_ailogger.Error(enfe.Message, enfe);
            //    statusCode = HttpStatusCode.NotFound;
            //    feedback.Add(new FeedbackItem { StatusCode = HttpStatusCode.NotFound, Description = enfe.Message, Type = FeedbackType.Warning });
            //}
            catch (UnauthorizedAccessException e)
            {
                return new UnauthorizedResult();
            }
            catch (ExceptionWithFeedback e)
            {
                var response = new Response<T>(e.Feedback, absolutePath);
                return new BadRequestObjectResult(response);
            }
            catch (Exception e)
                {
                    var response = new Response<IEnumerable<T>>(returnData, new FeedbackItem { Description = e.Message, Type = FeedbackType.ValidationError }, absolutePath);
                    int dataCount = response.Data?.Count() ?? 0;
                    response.Skip = 0;
                    response.Take = dataCount;
                    response.Total = dataCount;

                    return new BadRequestObjectResult(response);
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
