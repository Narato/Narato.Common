using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Abstractions;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Routing;
using Moq;
using Narato.Common.ActionFilters;
using System.Collections.Generic;
using System.Diagnostics;
using Xunit;

namespace Narato.Common.Test
{
    public class ExecutionTimingFilterTest
    {
        [Fact]
        public void OnActionExecutingAddsTheStopwatch()
        {
            var controller = new Mock<Controller>();

            var headerDictionary = new HeaderDictionary();

            var httpContextMock = new Mock<HttpContext>();
            httpContextMock.SetupGet(x => x.Items).Returns(new Dictionary<object, object>());

            controller.Object.ControllerContext = new ControllerContext()
            {
                HttpContext = httpContextMock.Object
            };
           
            var actionExecutingContext = new Mock<ActionExecutingContext>(new object[] { new ActionContext(httpContextMock.Object, new RouteData(), new ActionDescriptor()), new List<IFilterMetadata>(), new Dictionary<string, object>(), controller  });
            actionExecutingContext.SetupGet(x => x.Controller).Returns(controller);


            var filter = new ExecutionTimingFilter();

            filter.OnActionExecuting(actionExecutingContext.Object);
            Assert.Equal(httpContextMock.Object.Items.Count, 1);
            Assert.True(httpContextMock.Object.Items.ContainsKey("api_timing"));
            Assert.IsType(typeof(Stopwatch), httpContextMock.Object.Items["api_timing"]);

        }

        [Fact]
        public void OnActionExecutedGenerationIsFilled()
        {
            var controller = new Mock<Controller>();

            var headerDictionary = new HeaderDictionary();

            var httpContextMock = new Mock<HttpContext>();
            var stopwatch = new Stopwatch();
            stopwatch.Start();
            httpContextMock.SetupGet(x => x.Items).Returns(new Dictionary<object, object>() { {"api_timing", stopwatch} });

            controller.Object.ControllerContext = new ControllerContext()
            {
                HttpContext = httpContextMock.Object
            };

            var actionExecutedContext = new Mock<ActionExecutedContext>(new object[] { new ActionContext(httpContextMock.Object, new RouteData(), new ActionDescriptor()),  new List<IFilterMetadata>(),  controller });
            actionExecutedContext.SetupGet(x => x.Controller).Returns(controller);
            actionExecutedContext.SetupGet(x => x.Result).Returns(new ObjectResult(new Models.Response()));


            var filter = new ExecutionTimingFilter();

            filter.OnActionExecuted(actionExecutedContext.Object);
            Assert.Equal(httpContextMock.Object.Items.Count, 1);
            Assert.True(httpContextMock.Object.Items.ContainsKey("api_timing"));
            Assert.IsType(typeof(Stopwatch), httpContextMock.Object.Items["api_timing"]);
            Assert.NotNull(actionExecutedContext.Object.Result);
            Assert.IsType(typeof(ObjectResult), actionExecutedContext.Object.Result);
            var objectResult = actionExecutedContext.Object.Result as ObjectResult;
            Assert.NotNull(objectResult.Value);
            Assert.IsType(typeof(Models.Response), objectResult.Value);
            var responseModel = objectResult.Value as Models.Response;
            Assert.NotNull(responseModel.Generation);
            Assert.NotNull(responseModel.Generation.TimeStamp);
        }

        [Fact]
        public void OnActionExecutedWithoutStopwatchInItems()
        {
            var controller = new Mock<Controller>();

            var headerDictionary = new HeaderDictionary();

            var httpContextMock = new Mock<HttpContext>();
            var stopwatch = new Stopwatch();
            stopwatch.Start();
            httpContextMock.SetupGet(x => x.Items).Returns(new Dictionary<object, object>() { { "api_timing", null } });

            controller.Object.ControllerContext = new ControllerContext()
            {
                HttpContext = httpContextMock.Object
            };

            var actionExecutedContext = new Mock<ActionExecutedContext>(new object[] { new ActionContext(httpContextMock.Object, new RouteData(), new ActionDescriptor()), new List<IFilterMetadata>(), controller });
            actionExecutedContext.SetupGet(x => x.Controller).Returns(controller);
            actionExecutedContext.SetupGet(x => x.Result).Returns(new ObjectResult(new Models.Response()));


            var filter = new ExecutionTimingFilter();

            filter.OnActionExecuted(actionExecutedContext.Object);
        }

        [Fact]
        public void OnActionExecutedWithoutResponseInResult()
        {
            var controller = new Mock<Controller>();

            var headerDictionary = new HeaderDictionary();

            var httpContextMock = new Mock<HttpContext>();
            var stopwatch = new Stopwatch();
            stopwatch.Start();
            httpContextMock.SetupGet(x => x.Items).Returns(new Dictionary<object, object>() { { "api_timing", stopwatch } });

            controller.Object.ControllerContext = new ControllerContext()
            {
                HttpContext = httpContextMock.Object
            };

            var actionExecutedContext = new Mock<ActionExecutedContext>(new object[] { new ActionContext(httpContextMock.Object, new RouteData(), new ActionDescriptor()), new List<IFilterMetadata>(), controller });
            actionExecutedContext.SetupGet(x => x.Controller).Returns(controller);

            var filter = new ExecutionTimingFilter();

            filter.OnActionExecuted(actionExecutedContext.Object);
        }
    }
}
