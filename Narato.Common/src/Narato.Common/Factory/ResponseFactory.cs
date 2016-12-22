using Narato.Common.Models;
using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Narato.Common.Interfaces;
using System.Reflection;
using NLog;
using System.Threading.Tasks;

namespace Narato.Common.Factory
{
    public class ResponseFactory : IResponseFactory
    {
        protected static Logger Logger = LogManager.GetCurrentClassLogger();

        protected IExceptionHandler _exceptionHandler;
        protected IExceptionToActionResultMapper _exceptionMapper;

        public ResponseFactory(IExceptionHandler exceptionHandler, IExceptionToActionResultMapper exceptionMapper)
        {
            _exceptionHandler = exceptionHandler;
            _exceptionMapper = exceptionMapper;
        }

        public IActionResult CreateGetResponse<T>(Func<T> callback, string absolutePath)
        {
            var feedback = new List<FeedbackItem>();
            T returnData = default(T);

            try
            {
                returnData = _exceptionHandler.PrettifyExceptions<T>(callback);
                if (returnData != null)
                    return new ObjectResult(new Response<T>(returnData, absolutePath, 200));
                return new NotFoundObjectResult(new Response<T>(new FeedbackItem() { Description = "The object was not found", Type = FeedbackType.Info }, absolutePath, 404));
            }
            catch (Exception e) {
                Logger.Error(e);
                return _exceptionMapper.Map<T>(e, absolutePath);
            }
        }
        
        public async Task<IActionResult> CreateGetResponseAsync<T>(Func<Task<T>> callback, string absolutePath)
        {
            var feedback = new List<FeedbackItem>();
            T returnData = default(T);

            try
            {
                returnData = await _exceptionHandler.PrettifyExceptionsAsync<T>(callback);
                if (returnData != null)
                    return new ObjectResult(new Response<T>(returnData, absolutePath, 200));
                return new NotFoundObjectResult(new Response<T>(new FeedbackItem() { Description = "The object was not found", Type = FeedbackType.Info }, absolutePath, 404));
            }
            catch (Exception e) {
                Logger.Error(e);
                return _exceptionMapper.Map<T>(e, absolutePath);
            }
        }

        public IActionResult CreatePostResponse<T>(Func<T> callback, string absolutePath, string routeName, object routeValues, List<RouteValuesIdentifierPair> routeValueIdentifierPairs = null)
        {
            var feedback = new List<FeedbackItem>();

            try
            {
                var returndata = _exceptionHandler.PrettifyExceptions<T>(callback);

                if (!string.IsNullOrEmpty(routeName) && routeValueIdentifierPairs != null && routeValues != null)
                {
                    routeValueIdentifierPairs.ForEach(x =>
                    {
                        var propertyInfo = returndata.GetType().GetProperty(x.ModelIdentifier);
                        var modelIdentiefer = propertyInfo.GetValue(returndata);

                        var routePropertyInfo = routeValues.GetType().GetProperty(x.RouteValuesIdentifier);
                        routePropertyInfo.SetValue(routeValues, modelIdentiefer);
                    });

                    return new CreatedAtRouteResult(routeName, routeValues, new Response<T>(returndata, absolutePath, 201));
                }

                return new ObjectResult(new Response<T>(returndata, absolutePath, 201));
                
            }
            catch (Exception e) {
                Logger.Error(e);
                return _exceptionMapper.Map<T>(e, absolutePath);
            }
        }

        public IActionResult CreatePostResponse<T>(Func<T> callback, string absolutePath)
        {
            return CreatePostResponse(callback, absolutePath, string.Empty, string.Empty, null);
        }

        public async Task<IActionResult> CreatePostResponseAsync<T>(Func<Task<T>> callback, string absolutePath, string routeName, object routeValues, List<RouteValuesIdentifierPair> routeValueIdentifierPairs = null)
        {
            var feedback = new List<FeedbackItem>();

            try
            {
                var returndata = await _exceptionHandler.PrettifyExceptionsAsync<T>(callback);

                if (!string.IsNullOrEmpty(routeName) && routeValueIdentifierPairs != null && routeValues != null)
                {
                    routeValueIdentifierPairs.ForEach(x =>
                    {
                        var propertyInfo = returndata.GetType().GetProperty(x.ModelIdentifier);
                        var modelIdentiefer = propertyInfo.GetValue(returndata);

                        var routePropertyInfo = routeValues.GetType().GetProperty(x.RouteValuesIdentifier);
                        routePropertyInfo.SetValue(routeValues, modelIdentiefer);
                    });

                    return new CreatedAtRouteResult(routeName, routeValues, new Response<T>(returndata, absolutePath, 201));
                }

                return new ObjectResult(new Response<T>(returndata, absolutePath, 201));
            }
            catch (Exception e) {
                Logger.Error(e);
                return _exceptionMapper.Map<T>(e, absolutePath);
            }
        }

        public async Task<IActionResult> CreatePostResponseAsync<T>(Func<Task<T>> callback, string absolutePath)
        {
            return await CreatePostResponseAsync(callback, absolutePath, string.Empty, string.Empty, null);
        }

        public IActionResult CreatePutResponse<T>(Func<T> callback, string absolutePath)
        {
            var feedback = new List<FeedbackItem>();

            try
            {
                var returndata = _exceptionHandler.PrettifyExceptions<T>(callback);
                return new OkObjectResult(new Response<T>(returndata, absolutePath, 200));
            }
            catch (Exception e) {
                Logger.Error(e);
                return _exceptionMapper.Map<T>(e, absolutePath);
            }
        }

        public async Task<IActionResult> CreatePutResponseAsync<T>(Func<Task<T>> callback, string absolutePath)
        {
            var feedback = new List<FeedbackItem>();

            try
            {
                var returndata = await _exceptionHandler.PrettifyExceptionsAsync<T>(callback);
                return new OkObjectResult(new Response<T>(returndata, absolutePath, 200));
            }
            catch (Exception e) {
                Logger.Error(e);
                return _exceptionMapper.Map<T>(e, absolutePath);
            }
        }

        public IActionResult CreateDeleteResponse(Action callback, string absolutePath)
        {
            var feedback = new List<FeedbackItem>();
            try
            {
                _exceptionHandler.PrettifyExceptions(callback);
                return new NoContentResult();
            }
            catch (Exception e) {
                Logger.Error(e);
                return _exceptionMapper.Map<bool>(e, absolutePath);
            }
        }

        public async Task<IActionResult> CreateDeleteResponseAsync(Func<Task> callback, string absolutePath)
        {
            var feedback = new List<FeedbackItem>();
            try
            {
                await _exceptionHandler.PrettifyExceptionsAsync(callback);
                return new NoContentResult();
            }
            catch (Exception e) {
                Logger.Error(e);
                return _exceptionMapper.Map<bool>(e, absolutePath);
            }
        }

        public IActionResult CreateGetResponseForCollection<T>(Func<PagedCollectionResponse<IEnumerable<T>>> callback, string absolutePath)
        {
            try
            {
                var returnData = _exceptionHandler.PrettifyExceptions(callback);

                var response = new Response<IEnumerable<T>>(returnData, absolutePath, 200);

                return new ObjectResult(returnData);
            }
            catch (Exception e) {
                Logger.Error(e);
                return _exceptionMapper.Map<T>(e, absolutePath);
            }
        }

        public async Task<IActionResult> CreateGetResponseForCollectionAsync<T>(Func<Task<PagedCollectionResponse<IEnumerable<T>>>> callback, string absolutePath)
        {
            try
            {
                var returnData = await _exceptionHandler.PrettifyExceptionsAsync(callback);

                var response = new Response<IEnumerable<T>>(returnData, absolutePath, 200);

                return new ObjectResult(returnData);
            }
            catch (Exception e) {
                Logger.Error(e);
                return _exceptionMapper.Map<T>(e, absolutePath);
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
            var response = new Response(feedbackItems, null, 400); // TODO: this should ALSO have a Self

            return new BadRequestObjectResult(response);
        }

        public IActionResult CreateMissingParam(MissingParam missingParam)
        {
            return CreateMissingParam(new List<MissingParam> { missingParam });
        }
    }
}
