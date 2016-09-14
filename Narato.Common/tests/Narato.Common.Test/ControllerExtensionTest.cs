using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Xunit;

namespace Narato.Common.Test
{
    public class ControllerExtensionTest
    {
        [Fact]
        public void GetRequestUriReturnsNonEmptyString()
        {
            var controller = new Mock<Controller>();

            var headerDictionary = new HeaderDictionary();
            var request = new Mock<HttpRequest>();
            request.SetupGet(r => r.Headers).Returns(headerDictionary);
            request.SetupGet(x => x.Path).Returns("/formengine.api");
            request.SetupGet(x => x.QueryString).Returns(new QueryString("?parameter=value"));

            var httpContext = new Mock<HttpContext>();
            httpContext.SetupGet(a => a.Request).Returns(request.Object);

            controller.Object.ControllerContext = new ControllerContext()
            {
                HttpContext = httpContext.Object
            };

            var requestUri = controller.Object.GetRequestUri();

            Assert.NotNull(requestUri);
            Assert.Equal(requestUri, "/formengine.api" + "?parameter=value");
        }

        [Fact]
        public void GetRequestUriReturnsEmptyStringOnEmptyRequest()
        {
            var controller = new Mock<Controller>();

            var requestUri = controller.Object.GetRequestUri();

            Assert.NotNull(requestUri);
            Assert.Equal(requestUri, string.Empty);
        }
    }
}
