using Microsoft.AspNetCore.Mvc;
using Moq;
using Narato.Common.Exceptions;
using Narato.Common.Factory;
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

            var responseFactory = new ResponseFactory(exceptionHandler);

            var objectResult = responseFactory.CreateGetResponse(func, string.Empty);

            Assert.NotNull(objectResult);
        }

        [Fact]
        public void GetResponseReturnsBadRequestResultOnException()
        {
            Func<Response> func = () => { throw new Exception(); };

            var exceptionHandler = new ExceptionHandler();
            var responseFactory = new ResponseFactory(exceptionHandler);
            var badRequestResult = responseFactory.CreateGetResponse(func, string.Empty);

            Assert.NotNull(badRequestResult);
            Assert.IsType(typeof(BadRequestObjectResult), badRequestResult);
        }

        [Fact]
        public void GetResponseReturnsUnauthorizedResultOnUnauthorizedAccessExceptio()
        {
            Func<Response> func = () => { throw new UnauthorizedAccessException(); };


            var exceptionHandler = new ExceptionHandler();
            var responseFactory = new ResponseFactory(exceptionHandler);
            var unauthorizedResult = responseFactory.CreateGetResponse(func, string.Empty);

            Assert.NotNull(unauthorizedResult);
            Assert.IsType(typeof(UnauthorizedResult), unauthorizedResult);
        }

        [Fact]
        public void GetResponseReturnsNotFondResultWhenManagerReturnsNull()
        {
            Func<Response> func = () => { return null; };

            var exceptionHandler = new ExceptionHandler();

            var responseFactory = new ResponseFactory(exceptionHandler);
            var notFoundResult = responseFactory.CreateGetResponse(func, string.Empty);

            Assert.NotNull(notFoundResult);
            Assert.IsType(typeof(NotFoundObjectResult), notFoundResult);
        }

        [Fact]
        public void GetResponseForCollectionReturnsObjectResult()
        {
            var list = new List<Response>();
            Func< List < Response >> func = () => { return list; };

            var exceptionHandler = new ExceptionHandler();
            var responseFactory = new ResponseFactory(exceptionHandler);
            var objectResult = responseFactory.CreateGetResponseForCollection(func, string.Empty);

            Assert.NotNull(objectResult);
        }

        [Fact]
        public void GetResponseForCollectionReturnsBadRequestResultOnException()
        {
            Func<List<Response>> func = () => { throw new Exception(); };
            var exceptionHandler = new ExceptionHandler();
            var responseFactory = new ResponseFactory(exceptionHandler);
            var badRequestResult = responseFactory.CreateGetResponseForCollection(func, string.Empty);

            Assert.NotNull(badRequestResult);
            Assert.IsType(typeof(BadRequestObjectResult), badRequestResult);
        }

        [Fact]
        public void GetResponseForCollectionReturnsUnauthorizedOnUnauthorizedAccessException()
        {
            Func<List<Response>> func = () => { throw new UnauthorizedAccessException(); };
            var exceptionHandler = new ExceptionHandler();
            var responseFactory = new ResponseFactory(exceptionHandler);
            var unauthorizedResult = responseFactory.CreateGetResponseForCollection(func, string.Empty);

            Assert.NotNull(unauthorizedResult);
            Assert.IsType(typeof(UnauthorizedResult), unauthorizedResult);
        }

        [Fact]
        public void PostResponseReturnsCreatedAtRouteResult()
        {

            var response = It.IsAny<Response>();
            Func<Response> func = () => { return It.IsAny<Response>(); };
            var exceptionHandler = new ExceptionHandler();

            var responseFactory = new ResponseFactory(exceptionHandler);

            var createdAtRouteReesult = responseFactory.CreatePostResponse<Response>(func , string.Empty, string.Empty, string.Empty);

            Assert.NotNull(createdAtRouteReesult);
            Assert.IsType(typeof(CreatedAtRouteResult), createdAtRouteReesult);
        }

        [Fact]
        public void PostResponseReturnsBadRequestAtException()
        {
            Func<Response> func = () => { throw new Exception(); };

            var exceptionHandler = new ExceptionHandler();
            var responseFactory = new ResponseFactory(exceptionHandler);
            var badRequest = responseFactory.CreatePostResponse<Response>(func, string.Empty, string.Empty, string.Empty);

            Assert.NotNull(badRequest);
            Assert.IsType(typeof(BadRequestObjectResult), badRequest);
        }

        [Fact]
        public void PostResponseReturnsUnauthorizedOnUnauthorizedAccessExceptio()
        {
            Func<Response> func = () => { throw new UnauthorizedAccessException(); };
            var exceptionHandler = new ExceptionHandler();

            var responseFactory = new ResponseFactory(exceptionHandler);
            var unauthorized = responseFactory.CreatePostResponse<Response>(func , string.Empty, string.Empty, string.Empty);

            Assert.NotNull(unauthorized);
            Assert.IsType(typeof(UnauthorizedResult), unauthorized);
        }

        [Fact]
        public void MissingQueryStringParameterReturnsBadRequest()
        {
            var exceptionHandler = new ExceptionHandler();
            var responseFactory = new ResponseFactory(exceptionHandler);

            var badRequest = responseFactory.CreateMissingParam(new MissingParam("missing", MissingParamType.QuerystringParam));

            Assert.NotNull(badRequest);
            Assert.IsType(typeof(BadRequestObjectResult), badRequest);
        }

        [Fact]
        public void MissingBodyParameterReturnsBadRequest()
        {
            var exceptionHandler = new ExceptionHandler();
            var responseFactory = new ResponseFactory(exceptionHandler);
            var badRequest = responseFactory.CreateMissingParam(new MissingParam("missing", MissingParamType.Body));

            Assert.NotNull(badRequest);
            Assert.IsType(typeof(BadRequestObjectResult), badRequest);
        }

        [Fact]
        public void MissingParametersReturnsBadRequest()
        {
             var exceptionHandler = new ExceptionHandler();
            var responseFactory = new ResponseFactory(exceptionHandler);
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
    }
}
