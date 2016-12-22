using Microsoft.AspNetCore.Mvc;
using Moq;
using Narato.Common.ActionResult;
using Narato.Common.Exceptions;
using Narato.Common.Factory;
using Narato.Common.Mappers;
using Narato.Common.Models;
using System;
using System.Collections.Generic;
using Xunit;

namespace Digipolis.FormEngine.Common.Test
{
    // This project can output the Class library as a NuGet Package.
    // To enable this option, right-click on the project and select the Properties menu item. In the Build tab select "Produce outputs on build".
    public class ResponseFactoryTest
    {
        [Fact]
        public void GetResponseReturnsObjectResult()
        {
            var response = new Response();
            Func<Response> func = () => { return response; };

            var exceptionHandler = new ExceptionHandler();

            var exceptionMapper = new ExceptionToActionResultMapper();
            var responseFactory = new ResponseFactory(exceptionHandler, exceptionMapper);

            var objectResult = responseFactory.CreateGetResponse(func, string.Empty);

            Assert.NotNull(objectResult);
        }

        [Fact]
        public void GetResponseReturnsInternalServerErrorResultOnException()
        {
            Func<Response> func = () => { throw new Exception(); };

            var exceptionHandler = new ExceptionHandler();
            var exceptionMapper = new ExceptionToActionResultMapper();
            var responseFactory = new ResponseFactory(exceptionHandler, exceptionMapper);
            var badRequestResult = responseFactory.CreateGetResponse(func, string.Empty);

            Assert.NotNull(badRequestResult);
            Assert.IsType(typeof(InternalServerErrorWithResponse), badRequestResult);
        }

        [Fact]
        public void GetResponseReturnsUnauthorizedResultOnUnauthorizedAccessException()
        {
            Func<Response> func = () => { throw new UnauthorizedAccessException(); };

            var exceptionHandler = new ExceptionHandler();
            var exceptionMapper = new ExceptionToActionResultMapper();
            var responseFactory = new ResponseFactory(exceptionHandler, exceptionMapper);
            var unauthorizedResult = responseFactory.CreateGetResponse(func, string.Empty);

            Assert.NotNull(unauthorizedResult);
            Assert.IsType(typeof(UnauthorizedResult), unauthorizedResult);
        }

        [Fact]
        public void GetResponseReturnsNotFoundOnEntityNotFoundException()
        {
            Func<Response> func = () => { throw new EntityNotFoundException(); };

            var exceptionHandler = new ExceptionHandler();
            var exceptionMapper = new ExceptionToActionResultMapper();
            var responseFactory = new ResponseFactory(exceptionHandler, exceptionMapper);
            var notFoundResult = responseFactory.CreateGetResponse(func, string.Empty);

            Assert.NotNull(notFoundResult);
            Assert.IsType(typeof(NotFoundResult), notFoundResult);
        }

        [Fact]
        public void GetResponseReturnsNotFoundOnEntityNotFoundExceptionWithMessage()
        {
            Func<Response> func = () => { throw new EntityNotFoundException("test"); };

            var exceptionHandler = new ExceptionHandler();
            var exceptionMapper = new ExceptionToActionResultMapper();
            var responseFactory = new ResponseFactory(exceptionHandler, exceptionMapper);
            var notFoundResult = responseFactory.CreateGetResponse(func, string.Empty);

            Assert.NotNull(notFoundResult);
            Assert.IsType(typeof(NotFoundObjectResult), notFoundResult);
        }

        [Fact]
        public void GetResponseReturnsInternalServerErrorWitResponse()
        {
            Func<Response> func = () => { throw new ExceptionWithFeedback(new FeedbackItem()); };

            var exceptionHandler = new ExceptionHandler();
            var exceptionMapper = new ExceptionToActionResultMapper();
            var responseFactory = new ResponseFactory(exceptionHandler, exceptionMapper);
            var badRequestResult = responseFactory.CreateGetResponse(func, string.Empty);

            Assert.NotNull(badRequestResult);
            Assert.IsType(typeof(InternalServerErrorWithResponse), badRequestResult);
        }

        [Fact]
        public void GetResponseReturnsNotFondResultWhenManagerReturnsNull()
        {
            Func<Response> func = () => { return null; };

            var exceptionHandler = new ExceptionHandler();

            var exceptionMapper = new ExceptionToActionResultMapper();
            var responseFactory = new ResponseFactory(exceptionHandler, exceptionMapper);
            var notFoundResult = responseFactory.CreateGetResponse(func, string.Empty);

            Assert.NotNull(notFoundResult);
            Assert.IsType(typeof(NotFoundObjectResult), notFoundResult);
        }

        [Fact]
        public void GetResponseForCollectionReturnsObjectResult()
        {
            var list = new PagedCollectionResponse<IEnumerable<Response>>();
            Func<PagedCollectionResponse<IEnumerable<Response>>> func = () => { return list; };

            var exceptionHandler = new ExceptionHandler();
            var exceptionMapper = new ExceptionToActionResultMapper();
            var responseFactory = new ResponseFactory(exceptionHandler, exceptionMapper);
            var objectResult = responseFactory.CreateGetResponseForCollection(func, string.Empty);

            Assert.NotNull(objectResult);
        }

        [Fact]
        public void GetResponseForCollectionReturnsInternalServerErrorResultOnException()
        {
            Func<PagedCollectionResponse<IEnumerable<Response>>> func = () => { throw new Exception(); };
            var exceptionHandler = new ExceptionHandler();
            var exceptionMapper = new ExceptionToActionResultMapper();
            var responseFactory = new ResponseFactory(exceptionHandler, exceptionMapper);
            var badRequestResult = responseFactory.CreateGetResponseForCollection(func, string.Empty);

            Assert.NotNull(badRequestResult);
            Assert.IsType(typeof(InternalServerErrorWithResponse), badRequestResult);
        }

        [Fact]
        public void GetResponseForCollectionReturnsInternalServerErrorWithResponse()
        {
            Func<PagedCollectionResponse<IEnumerable<Response>>> func = () => { throw new ExceptionWithFeedback(new FeedbackItem()); };
            var exceptionHandler = new ExceptionHandler();
            var exceptionMapper = new ExceptionToActionResultMapper();
            var responseFactory = new ResponseFactory(exceptionHandler, exceptionMapper);
            var badRequestResult = responseFactory.CreateGetResponseForCollection(func, string.Empty);

            Assert.NotNull(badRequestResult);
            Assert.IsType(typeof(InternalServerErrorWithResponse), badRequestResult);
        }

        [Fact]
        public void GetResponseForCollectionReturnsNotFoundAtEntityNotFoundException()
        {
            Func<PagedCollectionResponse<IEnumerable<Response>>> func = () => { throw new EntityNotFoundException(); };
            var exceptionHandler = new ExceptionHandler();
            var exceptionMapper = new ExceptionToActionResultMapper();
            var responseFactory = new ResponseFactory(exceptionHandler, exceptionMapper);
            var notFoundResult = responseFactory.CreateGetResponseForCollection(func, string.Empty);

            Assert.NotNull(notFoundResult);
            Assert.IsType(typeof(NotFoundResult), notFoundResult);
        }

        [Fact]
        public void GetResponseForCollectionReturnsNotFoundAtEntityNotFoundExceptionWithMessage()
        {
            Func<PagedCollectionResponse<IEnumerable<Response>>> func = () => { throw new EntityNotFoundException("test"); };
            var exceptionHandler = new ExceptionHandler();
            var exceptionMapper = new ExceptionToActionResultMapper();
            var responseFactory = new ResponseFactory(exceptionHandler, exceptionMapper);
            var notFoundResult = responseFactory.CreateGetResponseForCollection(func, string.Empty);

            Assert.NotNull(notFoundResult);
            Assert.IsType(typeof(NotFoundObjectResult), notFoundResult);
        }

        [Fact]
        public void GetResponseForCollectionReturnsUnauthorizedOnUnauthorizedAccessException()
        {
            Func<PagedCollectionResponse<IEnumerable<Response>>> func = () => { throw new UnauthorizedAccessException(); };
            var exceptionHandler = new ExceptionHandler();
            var exceptionMapper = new ExceptionToActionResultMapper();
            var responseFactory = new ResponseFactory(exceptionHandler, exceptionMapper);
            var unauthorizedResult = responseFactory.CreateGetResponseForCollection(func, string.Empty);

            Assert.NotNull(unauthorizedResult);
            Assert.IsType(typeof(UnauthorizedResult), unauthorizedResult);
        }

        public class TestObject
        {
            public int Id { get; set; }
        }

        public class TestRouteValues
        {
            public string Controller { get; set; }
            public int Id { get; set; }
        }

        [Fact]
        public void PostResponseReturnsCreatedAtRouteResult()
        {
            var response = It.IsAny<Response>();
            Func<TestObject> func = () => { return new TestObject(); };
            var exceptionHandler = new ExceptionHandler();

            var exceptionMapper = new ExceptionToActionResultMapper();
            var responseFactory = new ResponseFactory(exceptionHandler, exceptionMapper);

            var createdAtRouteResult = responseFactory.CreatePostResponse(func, string.Empty, "CreatedAtRoute", new TestRouteValues { Controller = "TestController", Id = 1}, new List<RouteValuesIdentifierPair> { new RouteValuesIdentifierPair { ModelIdentifier = "Id", RouteValuesIdentifier = "Id" } });

            Assert.NotNull(createdAtRouteResult);
            Assert.IsType(typeof(CreatedAtRouteResult), createdAtRouteResult);
        }

        [Fact]
        public void PostResponseReturnsObjectResultWithCode201()
        {
            var response = It.IsAny<Response>();
            Func<Response> func = () => { return It.IsAny<Response>(); };
            var exceptionHandler = new ExceptionHandler();

            var exceptionMapper = new ExceptionToActionResultMapper();
            var responseFactory = new ResponseFactory(exceptionHandler, exceptionMapper);

            var objectResult = responseFactory.CreatePostResponse(func, string.Empty);

            Assert.NotNull(objectResult);
            Assert.IsType(typeof(ObjectResult), objectResult);
            Assert.IsType(typeof(Response<Response>), ((ObjectResult)objectResult).Value);

            Assert.Equal(((Response<Response>)((ObjectResult)objectResult).Value).Status, 201);
        }

        [Fact]
        public void PostResponseReturnsInternalServerErrorAtException()
        {
            Func<Response> func = () => { throw new Exception(); };

            var exceptionHandler = new ExceptionHandler();
            var exceptionMapper = new ExceptionToActionResultMapper();
            var responseFactory = new ResponseFactory(exceptionHandler, exceptionMapper);
            var badRequest = responseFactory.CreatePostResponse(func, string.Empty);

            Assert.NotNull(badRequest);
            Assert.IsType(typeof(InternalServerErrorWithResponse), badRequest);
        }


        [Fact]
        public void PostResponseReturnsInternalServerErrorAtExceptionWithFeedback()
        {
            Func<Response> func = () => { throw new ExceptionWithFeedback(new FeedbackItem()); };

            var exceptionHandler = new ExceptionHandler();
            var exceptionMapper = new ExceptionToActionResultMapper();
            var responseFactory = new ResponseFactory(exceptionHandler, exceptionMapper);
            var badRequest = responseFactory.CreatePostResponse(func, string.Empty);

            Assert.NotNull(badRequest);
            Assert.IsType(typeof(InternalServerErrorWithResponse), badRequest);
        }

        [Fact]
        public void PostResponseReturnsNotFoundAtEntityNotFoundException()
        {
            Func<Response> func = () => { throw new EntityNotFoundException(); };

            var exceptionHandler = new ExceptionHandler();
            var exceptionMapper = new ExceptionToActionResultMapper();
            var responseFactory = new ResponseFactory(exceptionHandler, exceptionMapper);
            var badRequest = responseFactory.CreatePostResponse(func, string.Empty);

            Assert.NotNull(badRequest);
            Assert.IsType(typeof(NotFoundResult), badRequest);
        }

        [Fact]
        public void PostResponseReturnsNotFoundAtEntityNotFoundExceptionWithMessage()
        {
            Func<Response> func = () => { throw new EntityNotFoundException("test"); };

            var exceptionHandler = new ExceptionHandler();
            var exceptionMapper = new ExceptionToActionResultMapper();
            var responseFactory = new ResponseFactory(exceptionHandler, exceptionMapper);
            var badRequest = responseFactory.CreatePostResponse(func, string.Empty);

            Assert.NotNull(badRequest);
            Assert.IsType(typeof(NotFoundObjectResult), badRequest);
        }

        [Fact]
        public void PostResponseReturnsUnauthorizedOnUnauthorizedAccessExceptio()
        {
            Func<Response> func = () => { throw new UnauthorizedAccessException(); };
            var exceptionHandler = new ExceptionHandler();

            var exceptionMapper = new ExceptionToActionResultMapper();
            var responseFactory = new ResponseFactory(exceptionHandler, exceptionMapper);
            var unauthorized = responseFactory.CreatePostResponse(func, string.Empty);

            Assert.NotNull(unauthorized);
            Assert.IsType(typeof(UnauthorizedResult), unauthorized);
        }

        [Fact]
        public void MissingQueryStringParameterReturnsBadRequest()
        {
            var exceptionHandler = new ExceptionHandler();
            var exceptionMapper = new ExceptionToActionResultMapper();
            var responseFactory = new ResponseFactory(exceptionHandler, exceptionMapper);

            var badRequest = responseFactory.CreateMissingParam(new MissingParam("missing", MissingParamType.QuerystringParam));

            Assert.NotNull(badRequest);
            Assert.IsType(typeof(BadRequestObjectResult), badRequest);
        }

        [Fact]
        public void MissingBodyParameterReturnsBadRequest()
        {
            var exceptionHandler = new ExceptionHandler();
            var exceptionMapper = new ExceptionToActionResultMapper();
            var responseFactory = new ResponseFactory(exceptionHandler, exceptionMapper);
            var badRequest = responseFactory.CreateMissingParam(new MissingParam("missing", MissingParamType.Body));

            Assert.NotNull(badRequest);
            Assert.IsType(typeof(BadRequestObjectResult), badRequest);
        }

        [Fact]
        public void MissingParametersReturnsBadRequest()
        {
            var exceptionHandler = new ExceptionHandler();
            var exceptionMapper = new ExceptionToActionResultMapper();
            var responseFactory = new ResponseFactory(exceptionHandler, exceptionMapper);
            var badRequest = responseFactory.CreateMissingParam(new List<MissingParam>() { new MissingParam("missing", MissingParamType.QuerystringParam) });

            Assert.NotNull(badRequest);
            Assert.IsType(typeof(BadRequestObjectResult), badRequest);
        }

        [Fact]
        public void EmptyConstructorResponseTest()
        {
            var response = new Response<object>();

            Assert.NotNull(response);
        }

        [Fact]
        public void DeleteResponseReturnsNoContentResultOnDeletedSuccess()
        {
            Action action = () => { };

            var exceptionHandler = new ExceptionHandler();
            var exceptionMapper = new ExceptionToActionResultMapper();
            var responseFactory = new ResponseFactory(exceptionHandler, exceptionMapper);
            var objectResult = responseFactory.CreateDeleteResponse(action, string.Empty);

            Assert.NotNull(objectResult);
            Assert.IsType(typeof(NoContentResult), objectResult);
        }

        [Fact]
        public void DeleteResponseReturnsInternalServerErrorOnException()
        {
            Action action = () => { throw new Exception(); };

            var exceptionHandler = new ExceptionHandler();
            var exceptionMapper = new ExceptionToActionResultMapper();
            var responseFactory = new ResponseFactory(exceptionHandler, exceptionMapper);
            var objectResult = responseFactory.CreateDeleteResponse(action, string.Empty);

            Assert.NotNull(objectResult);
            Assert.IsType(typeof(InternalServerErrorWithResponse), objectResult);
        }

        [Fact]
        public void DeleteResponseReturnsUnauthorizedResultOnUnauthorizedAccessException()
        {
            Action action = () => { throw new UnauthorizedAccessException(); };

            var exceptionHandler = new ExceptionHandler();
            var exceptionMapper = new ExceptionToActionResultMapper();
            var responseFactory = new ResponseFactory(exceptionHandler, exceptionMapper);
            var objectResult = responseFactory.CreateDeleteResponse(action, string.Empty);

            Assert.NotNull(objectResult);
            Assert.IsType(typeof(UnauthorizedResult), objectResult);
        }

        [Fact]
        public void DeleteResponseReturnsInternalServerErrortOnExceptionWithFeedback()
        {
            Action action = () => { throw new ExceptionWithFeedback(new FeedbackItem()); };

            var exceptionHandler = new ExceptionHandler();
            var exceptionMapper = new ExceptionToActionResultMapper();
            var responseFactory = new ResponseFactory(exceptionHandler, exceptionMapper);
            var objectResult = responseFactory.CreateDeleteResponse(action, string.Empty);

            Assert.NotNull(objectResult);
            Assert.IsType(typeof(InternalServerErrorWithResponse), objectResult);
        }

        [Fact]
        public void DeleteResponseReturnsNotFoundOnEntityNotFoundException()
        {
            Action action = () => { throw new EntityNotFoundException(); };

            var exceptionHandler = new ExceptionHandler();
            var exceptionMapper = new ExceptionToActionResultMapper();
            var responseFactory = new ResponseFactory(exceptionHandler, exceptionMapper);
            var objectResult = responseFactory.CreateDeleteResponse(action, string.Empty);

            Assert.NotNull(objectResult);
            Assert.IsType(typeof(NotFoundResult), objectResult);
        }

        [Fact]
        public void DeleteResponseReturnsNotFoundOnEntityNotFoundExceptionWithMessage()
        {
            Action action = () => { throw new EntityNotFoundException("test"); };

            var exceptionHandler = new ExceptionHandler();
            var exceptionMapper = new ExceptionToActionResultMapper();
            var responseFactory = new ResponseFactory(exceptionHandler, exceptionMapper);
            var objectResult = responseFactory.CreateDeleteResponse(action, string.Empty);

            Assert.NotNull(objectResult);
            Assert.IsType(typeof(NotFoundObjectResult), objectResult);
        }
    }
}
